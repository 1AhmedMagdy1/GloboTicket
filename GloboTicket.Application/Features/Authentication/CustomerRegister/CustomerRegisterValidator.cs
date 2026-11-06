using FluentValidation;
using GloboTicket.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.Authentication.CustomerRegister
{
    internal class CustomerRegisterValidator:AbstractValidator<CustomerRegisterCommand>
    {
        public CustomerRegisterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithErrorCode("FirstNameRequired")
                .WithMessage("First Name is required");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithErrorCode("LastNameRequired")
                .WithMessage("Last Name is required");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithErrorCode("EmailRequired")
                .WithMessage("Email is required")
                .EmailAddress()
                .WithErrorCode("EmailInvalid")
                .WithMessage("Email is invalid");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithErrorCode("PasswordRequired")
                .WithMessage("Password is required")
                .MinimumLength(8)
                .WithErrorCode("PasswordTooShort")
                .WithMessage("Password must be at least 8 characters long")
                .Must(PasswordFormatValidator.DoesPasswordHavedigit)
                .WithErrorCode("PasswordMissingDigit")
                .WithMessage("Password must contain at least one digit")
                .Must(PasswordFormatValidator.DoesPasswordHaveLowerCaseChar)
                .WithErrorCode("PasswordMissingLowerCase")
                .WithMessage("Password must contain at least one lowercase letter")
                .Must(PasswordFormatValidator.DoesPasswordHaveUpperCaseChar)
                .WithErrorCode("PasswordMissingUpperCase")
                .WithMessage("Password must contain at least one uppercase letter")
                .Must(PasswordFormatValidator.DoesPasswordHaveSpecialChar)
                .WithErrorCode("PasswordMissingSpecialChar")
                .WithMessage("Password must contain at least one special character");
        }
    }
}
