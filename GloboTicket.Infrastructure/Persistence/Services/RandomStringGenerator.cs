using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Services
{
    public class RandomStringGenerator
    {

        public static string GenerateRandomString()
        {
           var RandomBytes= RandomNumberGenerator.GetBytes(64);//88 char
            string Token = Convert.ToBase64String(RandomBytes);
            return Token;
        }
    }
}
