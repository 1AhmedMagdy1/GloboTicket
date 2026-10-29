using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Identity;
using GloboTicket.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.SuspendUser
{
    internal class SuspendUserHandler : IRequestHandler<SuspenionUserQuery, Result<bool>>
    {
        private readonly IUserRepository userRepository;
     

        public SuspendUserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Result<bool>> Handle(SuspenionUserQuery request, CancellationToken cancellationToken)
        {

            var ActorValidAccess = await userRepository.HasRoleAsync(SystemRolesEnum.SystemAdmin.ToString(), cancellationToken);
            if (!ActorValidAccess.IsSuccess)
                return ActorValidAccess;

            var result = await userRepository.SuspendUserAsync(request.UserID,cancellationToken);
            return result;
        }
    }
}
