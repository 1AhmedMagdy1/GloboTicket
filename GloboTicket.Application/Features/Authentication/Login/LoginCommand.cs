using GloboTicket.Application.Common;
using GloboTicket.Application.DTOS.Authentitcation.Login;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.Authentication.Login
{
    public sealed record LoginCommand : IRequest<Result<LogincommandResult>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;



    }
    public sealed record LogincommandResult
    {
        public string Uid { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = new List<string>();
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTimeOffset RefreshTokenExpirationDate { get; set; }
    }
}
