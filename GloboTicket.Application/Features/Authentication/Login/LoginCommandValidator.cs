using FluentValidation;
using GloboTicket.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.Authentication.Login
{
    public class LoginCommandValidator:AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage("Email is required.")
                    .WithErrorCode("Auth.Validation.RequiredEmail")
                .EmailAddress()
                    .WithMessage("Please enter a valid email address.")
                    .WithErrorCode("Auth.Validation.InvalidEmailFormat");

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage("Password is required.")
                    .WithErrorCode("Auth.Validation.RequiredPassword");

        }

    }
}
