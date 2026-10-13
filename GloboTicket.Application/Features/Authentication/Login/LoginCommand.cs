using GloboTicket.Application.DTOS.Authentitcation.Login;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.Authentication.Login
{
    public sealed record LoginCommand : IRequest<LoginResponseDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }



    }
}
