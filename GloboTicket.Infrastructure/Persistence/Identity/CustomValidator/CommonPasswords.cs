using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Identity.CustomValidator
{
    public class CommonPasswords
    {
        public  static  HashSet<string> Passwords = new(StringComparer.OrdinalIgnoreCase)
        {
            // Common passwords
            "password",
            "password1",
            "password123",
            "Password123",
            "admin",
            "admin123",
            "administrator",
            "root",
            "qwerty",
            "qwerty123",
            "letmein",
            "welcome",
            "welcome123",
            "iloveyou",
            "monkey",
            "dragon",
            "master",
            "login",
            "passw0rd",
            "changeme",

            // Numeric sequences
            "123456",
            "1234567",
            "12345678",
            "123456789",
            "1234567890",
            "111111",
            "11111111",
            "000000",
            "00000000",
            "121212",
            "123123",
            "654321",
            "987654321",
            "112233",
            "123321",

            // Keyboard patterns
            "asdfgh",
            "asdfghjkl",
            "zxcvbn",
            "qwertyuiop",
            "1q2w3e4r",
            "1q2w3e4r5t",
            "qazwsx",
            "!qaz2wsx",

            // Common names and predictable words
            "secret",
            "secret123",
            "football",
            "baseball",
            "soccer",
            "basketball",
            "sunshine",
            "princess",
            "flower",
            "shadow",
            "trustno1",
            "whatever",
            "freedom",
            "starwars",
            "computer",

            // Common variations
            "abc123",
            "abc123456",
            "Password@123",
            "P@ssw0rd",
            "P@ssword123",
            "Admin@123",
            "Welcome@123",
            "Test1234",
            "Test@123",
            "User1234",
            "User@123",

            // Repeated characters
            "aaaaaa",
            "aaaaaaa",
            "aaaaaaaa",
            "bbbbbbbb",
            "abcdef",
            "abcdefgh",
            "abcd1234"
        };

        public static bool IsCommonPassword(string psd)
        {
            return Passwords.Contains(psd);
        }
    }
}
