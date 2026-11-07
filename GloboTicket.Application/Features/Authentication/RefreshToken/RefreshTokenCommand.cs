using GloboTicket.Application.Common;
using GloboTicket.Application.Features.Authentication.Login;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.Authentication.RefreshToken
{
    public  class RefreshTokenCommand:IRequest<Result<LogincommandResult>>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
