using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Helpers
{
    public class PasswordFormatValidator
    {
       public const string specialCharacters = "!@#$%^&*()-_+=[]{};:,.?";

        public static bool DoesPasswordHaveLowerCaseChar(string password)
        {
            foreach (char c in password)
                if (char.IsLower(c)) return true;

            return false;
        }
        public static bool DoesPasswordHaveUpperCaseChar(string password)
        {
            foreach (char c in password)
                if (char.IsUpper(c)) return true;

            return false;
        }
        public static bool DoesPasswordHaveSpecialChar(string password)
        {

            return password.Any(c => specialCharacters.Contains(c));
        }

        public static bool DoesPasswordHavedigit(string password)
        {
            for (int i = 0; i < password.Length; i++)
                if (char.IsDigit(password[i])) return true;

            return false;
        }

    }
}
