using GloboTicket.Application.Common;
using GloboTicket.Application.DTOS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.SuspendUser
{
    public sealed record SuspenionUserQuery:IRequest<Result<bool>>
    {
        

        public string UserID { get; set; } = string.Empty;
        public SuspensionActions Status { get; set; }

        public SuspenionUserQuery(string userID, SuspensionActions status)
        {
            UserID = userID;
            Status = status;
        }

    }
}
