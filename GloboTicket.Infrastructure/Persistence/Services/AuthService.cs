using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Identity;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Application.DTOS.UserManagement;
using GloboTicket.Application.Features.Authentication.CustomerRegister;
using GloboTicket.Application.Features.Authentication.Login;
using GloboTicket.Infrastructure.Persistence.Identity;
using GloboTicket.Infrastructure.Persistence.Identity.TokenOptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOptions<JwtOptions> jwtSettings;
   
        private readonly ApplicationDbContext context;
        public AuthService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwtSettings, ApplicationDbContext context)
        {
            _userRepository = userRepository;
            this.userManager = userManager;
            this.jwtSettings = jwtSettings;
            this.context = context;
        }

        public Task<string> GenerateJwtToken(UserInfo userInfo, CancellationToken cancellationToken)
        {
          var claims = new List<Claim>
            {
                new Claim("uid",userInfo.ID),
                new Claim(ClaimTypes.Email,userInfo.Email),
            };
            if (userInfo.Roles.Contains(SystemRolesEnum.TenantAdmin))
                claims.Add(new Claim("tid", userInfo.TenantID.ToString()!));

            foreach (var role in userInfo.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var signingkey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.SigningKey));
            var signingCredentials = new SigningCredentials(signingkey,SecurityAlgorithms.Sha256);
            var securityToken = new JwtSecurityToken(
                issuer: jwtSettings.Value.Issuer,
                audience: jwtSettings.Value.Audience,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(5)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            


            return Task.FromResult(token);
        }

        public async Task<Tuple<string, DateTimeOffset>> AddRefreshTokenAsync(string uid, CancellationToken cancellationToken)
        {
            var token = RandomStringGenerator.GenerateRandomString();
            var TokenBytes = Encoding.UTF8.GetBytes(token);
            var NormalizedToken = WebEncoders.Base64UrlEncode(TokenBytes);

            var HashedToken =Convert.ToBase64String( SHA256.HashData(TokenBytes));
            var refreshToken = new RefreshToken
            {
                TokenHash = HashedToken,
                UserID = uid,
                CreatedAt = DateTimeOffset.UtcNow,
                ExpiresAt=DateTimeOffset.UtcNow.AddHours(2)
                
            };
            context.RefreshTokens.Add(refreshToken);
            return new Tuple<string, DateTimeOffset>(
            NormalizedToken,
            refreshToken.ExpiresAt
        );

        }

        public Task<Result<bool>> RegisterCustomerAsync(CustomerRegisterCommand command,CancellationToken cancellationToken)
        {
            return _userRepository.AddUserAsync(command.FirstName, command.LastName, command.Email, command.Password, null, cancellationToken);
        }

        public async Task<bool> ValidateLoginCredentials(string email, string password, CancellationToken cancellationToken)
        {
            var user =await userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return false;
            }
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<Result<LogincommandResult>> RefreshToken(string HasToken, CancellationToken cancellationToken)
        {
           
           

          var RefreshTokenEntity= await context.RefreshTokens.FirstOrDefaultAsync(r => r.TokenHash == HasToken);

            if(RefreshTokenEntity is null)
                return Result<LogincommandResult>.UnAuthorized();

            RefreshTokenEntity.RevokedAt = DateTimeOffset.UtcNow;//Revoke the current refresh token

            //generate new refresh token
            var userInfo =await _userRepository.GetUserById(RefreshTokenEntity.UserID, cancellationToken);

            if(!userInfo.IsSuccess)
                return Result<LogincommandResult>.UnAuthorized();

            var accessToken =await GenerateJwtToken(userInfo.Data!, cancellationToken);

            var RefreshTokenResult = await AddRefreshTokenAsync(RefreshTokenEntity.UserID, cancellationToken);
            if(RefreshTokenResult is null)
                return Result<LogincommandResult>.UnAuthorized();

            var result = new LogincommandResult
            {
                Uid = userInfo.Data!.ID,
                AccessToken = accessToken,
                RefreshToken = RefreshTokenResult.Item1,
                RefreshTokenExpirationDate = RefreshTokenResult.Item2,
                Roles = userInfo.Data.Roles.Select(r => r.ToString()).ToList()
            };
           
            return Result<LogincommandResult>.Success(result,ResultType.OK, "Token Refreshed Successfully");
        }

        public async Task<Result<bool>> RevokeRefreshTokenAsync(string HashToken, CancellationToken cancellationToken = default)
        {
            var RefreshTokenEntity = await context.RefreshTokens.FirstAsync(r => r.TokenHash == HashToken);
            if (RefreshTokenEntity is null)
                return Result<bool>.UnAuthorized();
            RefreshTokenEntity.RevokedAt = DateTimeOffset.UtcNow;
            return Result<bool>.Success(true,ResultType.OK);
        }
    }
}
