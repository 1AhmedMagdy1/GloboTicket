using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Identity.CustomValidator
{
    public class CustomPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string? password)
        {
            if (!string.IsNullOrWhiteSpace(password)&&password.Contains(user.UserName!,StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "PasswordContainsUserName",
                        Description = "Password cannot contain your username."
                    }

                    ));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
