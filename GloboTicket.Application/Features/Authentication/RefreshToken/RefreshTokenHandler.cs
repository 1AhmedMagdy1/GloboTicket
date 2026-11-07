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

namespace GloboTicket.Application.Features.Authentication.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Result<LogincommandResult>>
    {
        private readonly IAuthService authService;
        private readonly IUnitOfWork unitOfWork;
        public RefreshTokenHandler(IAuthService authService, IUnitOfWork unitOfWork)
        {
            this.authService = authService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<LogincommandResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var DenormalizedTokenBytes = WebEncoders.Base64UrlDecode(request.RefreshToken);

            var HashedDenormalizedToken = SHA256.HashData(DenormalizedTokenBytes);

            if (HashedDenormalizedToken is null)
                return Result<LogincommandResult>.UnAuthorized();
            string HashToken = Convert.ToBase64String(HashedDenormalizedToken);

            var result = await authService.RefreshToken(HashToken, cancellationToken);
            if (!result.IsSuccess) return result;
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
