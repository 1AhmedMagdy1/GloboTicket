using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Identity.TokenOptions
{
    public class JwtOptions
    {
        public const string SectionName = "JWT";
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string SigningKey { get; set; } = string.Empty;
        public int ExpirationInMinutes { get; set; } = 5;

    }
}
