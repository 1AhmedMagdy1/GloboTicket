using GloboTicket.Application.Common;
using GloboTicket.Application.DTOS.UserManagement;
using GloboTicket.Application.Features.Authentication.CustomerRegister;
using GloboTicket.Application.Features.Authentication.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Contracts.Services
{
    public interface IAuthService
    {
        public Task<Result<bool>> RegisterCustomerAsync(CustomerRegisterCommand command, CancellationToken cancellationToken);
        public Task<bool> ValidateLoginCredentials(string email, string password, CancellationToken cancellationToken);
        public Task<string> GenerateJwtToken(UserInfo userInfo, CancellationToken cancellationToken);

        public Task<Tuple<string, DateTimeOffset>> AddRefreshTokenAsync(string uid, CancellationToken cancellationToken);

        public Task<Result<LogincommandResult>> RefreshToken(string HashToken, CancellationToken cancellationToken);

        public Task<Result<bool>> RevokeRefreshTokenAsync(string HashToken, CancellationToken cancellationToken = default);
    }
}
