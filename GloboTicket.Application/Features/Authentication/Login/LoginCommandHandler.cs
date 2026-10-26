using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.DTOS.Authentitcation.Login;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.Authentication.Login
{
    internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDTO>
    {
        private readonly IIdentityRepository identityRepository;

        public LoginCommandHandler(IIdentityRepository identityRepository)
        {
            this.identityRepository = identityRepository;
        }

        public Task<LoginResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
