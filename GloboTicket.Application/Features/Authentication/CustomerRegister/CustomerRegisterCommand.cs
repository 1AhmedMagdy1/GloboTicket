using GloboTicket.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.Authentication.CustomerRegister
{
    public sealed record CustomerRegisterCommand:IRequest<Result<bool>>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
