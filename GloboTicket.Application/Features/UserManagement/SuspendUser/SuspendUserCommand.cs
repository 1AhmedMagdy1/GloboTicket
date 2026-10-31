using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Identity;
using GloboTicket.Application.Contracts.Identity.Authorization;
using GloboTicket.Application.DTOS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.SuspendUser
{
    public sealed record SuspenionUserCommand:IRequest<Result<bool>>,IRequireAuthorization
    {
        

        public string UserID { get; set; } = string.Empty;
        public string Reason { get; set; }

        public IReadOnlyCollection<string> RequiredRoles => [SystemRolesEnum.SystemAdmin.ToString()];

        public SuspenionUserCommand(string userID, string reason)
        {
            UserID = userID;
            Reason = reason;
        }

    }
}
