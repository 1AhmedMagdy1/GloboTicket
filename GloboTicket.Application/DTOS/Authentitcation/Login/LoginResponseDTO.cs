using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.DTOS.Authentitcation.Login
{
    public class LoginResponseDTO
    {
        public string Uid { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = new List<string>();
        public string AccessToken { get; set; } = string.Empty;
        public DateTimeOffset RefreshTokenExpirationDate { get; set; }

    }
}
