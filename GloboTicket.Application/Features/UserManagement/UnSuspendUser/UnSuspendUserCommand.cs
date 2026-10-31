using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Identity;
using GloboTicket.Application.Contracts.Identity.Authorization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.UnSuspendUser
{
    public class UnSuspendUserCommand:IRequest<Result<bool>>,IRequireAuthorization
    {
        public string UserID { get; set; } = string.Empty;

        public IReadOnlyCollection<string> RequiredRoles => [SystemRolesEnum.SystemAdmin.ToString()];
    }
}
