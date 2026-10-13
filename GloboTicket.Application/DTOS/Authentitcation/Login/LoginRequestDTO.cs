using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.DTOS.Authentitcation.Login
{
    public class LoginRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
