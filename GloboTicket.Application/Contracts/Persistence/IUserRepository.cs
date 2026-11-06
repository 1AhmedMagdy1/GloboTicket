using GloboTicket.Application.Common;
using GloboTicket.Application.DTOS.UserManagement;
using GloboTicket.Application.Features.UserManagement.GetUsers;
using GloboTicket.Application.Features.UserManagement.UpdateUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        public Task<Result<Pagination<UserInfo>>> GetUsersAsync(GetUsersQuery query,CancellationToken cancellationToken);
        public Task<Result<bool>> HasRole(string uid,string Role, CancellationToken cancellationToken);
        public Task<Result<bool>> HasAnyRole(string uid,IEnumerable<string> Roles, CancellationToken cancellationToken);
        public Task<Result<bool>> HasRoleAsync(string Role, CancellationToken cancellationToken);
        public Task<bool> ExistsByIDAsync(string uid, CancellationToken cancellationToken);
        public Task<bool> ExistsByEmailAsync(string Email, CancellationToken cancellationToken=default);
        public Task<Result<UserInfo>> GetUserById(string uid, CancellationToken cancellationToken);
        public Task<Result<bool>> SuspendUserAsync(string uid,string Reason,string ActorID, CancellationToken cancellationToken);
        public Task<Result<bool>> UnSuspendUserAsync(string uid,string ActorID, CancellationToken cancellationToken);
        public Task<Result<bool>> UpdateUserAsync(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken);
        public Task<Result<List<string>>> GetUserRolesAsync(string uid, CancellationToken cancellationToken);
        public Task<Result<bool>> AddUserAsync(string FirstName, string LastName, string Email,string Password,int? TenantID,CancellationToken token);

        public Task<Result<UserInfo>> GetUserByEmailAsync(string Email, CancellationToken cancellationToken=default);


    }
}
