using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Identity.CustomValidator
{
    public class CommonPasswordValidator : IPasswordValidator<ApplicationUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string? password)
        {
            if (!string.IsNullOrWhiteSpace(password) && CommonPasswords.IsCommonPassword(password)){

                return Task.FromResult(IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "CommonPassword",
                        Description="This is a weak Password,try another one."
                    }
                    ));

            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
