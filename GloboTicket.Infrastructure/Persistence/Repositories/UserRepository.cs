using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Identity;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Application.DTOS.UserManagement;
using GloboTicket.Application.Features.UserManagement.GetUsers;
using GloboTicket.Application.Features.UserManagement.UpdateUser;
using GloboTicket.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ICurrentUserService currentUserService;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ICurrentUserService currentUserService)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.currentUserService = currentUserService;
        }


        public async Task<Result<Pagination<UserInfo>>> GetUsersAsync(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var Query =  _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                request.Search = request.Search.Trim();
                Query = Query.Where(u => u.Email!.StartsWith(request.Search)||u.FirstName.StartsWith(request.Search));
            }
            if (request.IsSuspended.HasValue)
                Query = Query.Where(u => u.IsSuspended == request.IsSuspended.Value);
            if (request.Roles is not null)
            {
                var Roles = request.Roles.Select(r => r.ToString()).Distinct().ToList();
                if (Roles is not null)
                {
                    var RolesList = await _context.Roles.Where(role => Roles.Contains(role.Name ?? "")).Select(r => r.Id).ToListAsync(cancellationToken);

                    Query = Query.Where(u=>
                        _context.UserRoles
                        .Any(ur =>u.Id==ur.UserId &&RolesList.Contains(ur.RoleId)));
                }
            }
                var TotalCount =await Query.CountAsync();
                var QueryResult = await Query
                    .OrderByDescending(o=>o.CreatedAt)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(r => new
                    {
                        ID = r.Id,
                        CreatedAt = r.CreatedAt,
                        Email = r.Email,
                        IsSuspended = r.IsSuspended,
                        Roles = _context.UserRoles.Where(ur => ur.UserId == r.Id)
                .Join(_context.Roles, ur => ur.RoleId, role => role.Id, (ur, role) => role.Name)
                .ToList(),
                    }).ToListAsync(cancellationToken);

                List<UserInfo> Users = QueryResult.Select(res=>new UserInfo
                {
                    ID=res.ID,
                    Email=res.Email??"",
                    CreatedAt=res.CreatedAt,
                    IsSuspended=res.IsSuspended,
                    Roles = res.Roles.Select(r=>Enum.Parse<SystemRolesEnum>(r!,true)).ToList()
                }).ToList();

                var Response = new Pagination<UserInfo>(request.PageNumber, request.PageSize, Users, TotalCount);

                return Result<Pagination<UserInfo>>.Success(Response, ResultType.OK, "Users Retrieved Successfully");
            }

        public async Task<Result<bool>> HasAnyRole(string uid, IEnumerable<string> Roles, CancellationToken cancellationToken)
        {
            var NormalizedRoles = Roles.Select(r => r.ToUpperInvariant());
            var res = await _context.UserRoles.Where(ur => ur.UserId == uid)
                .Join(_context.Roles, ur => ur.RoleId, role => role.Id, (ur, role) =>role.NormalizedName)
                .AnyAsync(role=> NormalizedRoles.Contains(role),cancellationToken);
            if (res)
                return Result<bool>.Success(true, ResultType.Accepted);
            return Result<bool>.Failure(false, ResultType.Forbidden,null);
        }

        public async Task<Result<bool>> HasRole(string uid, string Role, CancellationToken cancellationToken)
        {
            var user =await userManager.FindByIdAsync(uid);
            if(user == null)
            {
                return Result<bool>.Forbidden();
            }
            var IsExists= await userManager.IsInRoleAsync(user, Role);
            if (!IsExists)return Result<bool>.Forbidden();

            return Result<bool>.Success(true,ResultType.OK);
        }

        public  async Task<Result<bool>> HasRoleAsync(string Role, CancellationToken cancellationToken)
        {
            var Userid = currentUserService.getCurrentUserId();
            if (string.IsNullOrWhiteSpace(Userid))
            {
                return  Result<bool>.Forbidden();
            }

            return await HasRole(Userid, Role, cancellationToken);
        }

        public async Task<bool> IsuserExistsAsync(string uid, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(uid);
            return user != null;
        }

        public async Task<Result<bool>> SuspendUserAsync(string uid,string Reason,string ActorID, CancellationToken cancellationToken)
        {
            

            var Issuspened = await _context.SuspensionLogs.AnyAsync(sus => sus.AffectedUserId == uid && sus.UnsuspendedAt == null,cancellationToken);
            if (Issuspened)
            {
                var error = new ErrorResult("USER_ALREADY_SUSPENDED", "User is already suspended.");
                return Result<bool>.Conflict(error.ErrorMessage, error);
            }
            var Log = new SuspensionLog
            {
                AffectedUserId = uid,
                Reason = Reason,
                SuspendedAt = DateTimeOffset.Now,
                SuspensionActorID=ActorID
                
            };
             _context.SuspensionLogs.Add(Log);
            return Result<bool>.Success(true, ResultType.OK, "User Suspended Successfully");
                

            
        }

        public async Task<Result<bool>> UnSuspendUserAsync(string uid, string ActorID, CancellationToken cancellationToken)
        {
            var suspensionLog = await _context.SuspensionLogs.FirstOrDefaultAsync(sus => sus.AffectedUserId == uid && sus.UnsuspendedAt == null, cancellationToken);
            if(suspensionLog is null)
            {
                var error = new ErrorResult("USER_ALREADY_UNSUSPENDED", "User is already Unsuspended.");
                return Result<bool>.Conflict(error.ErrorMessage, error);
            }
            suspensionLog.UnsuspendedAt = DateTimeOffset.Now;
            suspensionLog.UnsuspensionActorID = ActorID;
            return Result<bool>.Success(true, ResultType.OK, "User UnSuspended Successfully");
                 
        }

        public async Task<Result<bool>> UpdateUserAsync(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            var User = await userManager.Users.
                Include(u=>u.PhoneNumbers)
                .FirstOrDefaultAsync(u=>u.Id==updateUserCommand.UserID);
            if(User is null)
                return Result<bool>.NotFound("User is Not Found", new ErrorResult("User.NotFound", "User is Not Found"));

            if (User.FirstName != updateUserCommand.FirstName)
                User.FirstName = updateUserCommand.FirstName;

            if (User.LastName != updateUserCommand.LastName)
                User.LastName = updateUserCommand.LastName;


            return Result<bool>.Success(true,ResultType.NoContent);
        }

        async Task<Result<UserInfo>> IUserRepository.GetUserById(string uid, CancellationToken cancellationToken)
        {


            var user = await _context.Users.AsNoTracking().Select(u => new
            {
                ID = u.Id,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                Roles = _context.UserRoles.Where(ur => ur.UserId == uid)
                .Join(_context.Roles, ur => ur.RoleId, role => role.Id, (ur, role) => role.Name)
                .ToList(),
                IsSuspended = u.IsSuspended

            }).FirstOrDefaultAsync(cancellationToken);

            if (user is null)
                return Result<UserInfo>.Failure(null, ResultType.NotFound, "User Not Found");

            var RolesNames = user.Roles.Select(r => Enum.Parse<SystemRolesEnum>(r!,true)).ToList();
            var Result = new UserInfo
            {
                ID = user.ID,
                CreatedAt = user.CreatedAt,
                Email = user.Email??"",
                Roles = RolesNames
            };
            return Result<UserInfo>.Success(Result, ResultType.OK, "User Retrieved Successfully");
        }
    }
}
