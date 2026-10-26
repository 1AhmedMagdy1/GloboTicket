using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.DTOS.UserManagement;
using GloboTicket.Application.Features.UserManagement.GetUsers;
using GloboTicket.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
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


        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public Task<UserInfo?> GetUserById(string uid, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Pagination<UserInfo>> GetUsersAsync(GetUsersQuery query, string actor, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasRole(string uid, string Role, CancellationToken cancellationToken)
        {
            var user =await userManager.FindByIdAsync(uid);
            if(user == null)
            {
                return false;
            }
            return await userManager.IsInRoleAsync(user, Role);
        }

        public async Task<bool> IsuserExists(string uid, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(uid);
            return user != null;
        }
    }
}
