using GloboTicket.Application.Common;
using GloboTicket.Application.DTOS.UserManagement;
using GloboTicket.Application.Features.UserManagement.GetUsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Contracts.Persistence
{
    public interface IUserRepository
    {
        public Task<Pagination<UserInfo>> GetUsersAsync(GetUsersQuery query, string actor,CancellationToken cancellationToken);
        public Task<bool> HasRole(string uid,string Role, CancellationToken cancellationToken);
        public Task<bool>IsuserExists(string uid, CancellationToken cancellationToken);
        public Task<UserInfo?> GetUserById(string uid, CancellationToken cancellationToken);
    }
}
