using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.UpdateUser
{
    public class UpdateUserValidator:AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            // User ID
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithErrorCode("USER_ID_REQUIRED")
                .WithMessage("User ID is required.");

            // First Name
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("FIRST_NAME_REQUIRED")
                .WithMessage("First name is required.")
                .MaximumLength(100)
                .WithErrorCode("FIRST_NAME_MAX_LENGTH")
                .WithMessage("First name must not exceed 100 characters.");

            // Last Name
            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithErrorCode("LAST_NAME_REQUIRED")
                .WithMessage("Last name is required.")
                .MaximumLength(100)
                .WithErrorCode("LAST_NAME_MAX_LENGTH")
                .WithMessage("Last name must not exceed 100 characters.");

            //// Phone Numbers List
            //RuleFor(x => x.PhoneNumbers)
            //    .Cascade(CascadeMode.Stop)
            //    .NotNull()
            //    .WithErrorCode("PHONE_NUMBERS_REQUIRED")
            //    .WithMessage("Phone numbers list is required.")
            //    .NotEmpty()
            //    .WithErrorCode("PHONE_NUMBERS_EMPTY")
            //    .WithMessage("At least one phone number is required.");

            // Validate Each Phone Number
            //RuleForEach(x => x.PhoneNumbers)
            //    .Cascade(CascadeMode.Stop)
            //    .NotEmpty()
            //    .WithErrorCode("PHONE_NUMBER_REQUIRED")
            //    .WithMessage("Phone number cannot be empty.")
            //     .When(x => x.PhoneNumbers != null);
        }
    }
}
