using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Application.Features.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GloboTicket.Application.Features.Authentication.RevokeRefreshToken
{
    public class RevokeHandler : IRequestHandler<RevokeCommand, Result<bool>>
    {
        private readonly IAuthService authService;
        private readonly IUnitOfWork unitOfWork;
        public RevokeHandler(IAuthService authService, IUnitOfWork unitOfWork)
        {
            this.authService = authService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(RevokeCommand request, CancellationToken cancellationToken)
        {
            var DenormalizedTokenBytes = WebEncoders.Base64UrlDecode(request.RefreshToken);

            var HashedDenormalizedToken = SHA256.HashData(DenormalizedTokenBytes);

            if (HashedDenormalizedToken is null)
                return Result<bool>.UnAuthorized();
            string HashToken = Convert.ToBase64String(HashedDenormalizedToken);

            var authResult = await authService.RevokeRefreshTokenAsync(HashToken, cancellationToken);
            if (authResult.IsSuccess)
                await unitOfWork.SaveChangesAsync();
            return authResult;
        }
    }
}
