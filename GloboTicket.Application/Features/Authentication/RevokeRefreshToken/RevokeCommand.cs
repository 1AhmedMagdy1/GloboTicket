using GloboTicket.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.Authentication.RevokeRefreshToken
{
    public class RevokeCommand:IRequest<Result<bool>>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
