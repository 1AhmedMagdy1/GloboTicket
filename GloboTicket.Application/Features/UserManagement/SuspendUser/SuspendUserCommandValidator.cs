using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.SuspendUser
{
    public class SuspendUserCommandValidator:AbstractValidator<SuspenionUserCommand>
    {
        public SuspendUserCommandValidator()
        {
            RuleFor(x => x.UserID)
                .NotEmpty()
                .WithErrorCode("User.UserID.Required")
                .WithMessage("User ID is Required");

            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithErrorCode("User.Reason.Required")
                .WithMessage("Reason is Required")
                .MaximumLength(100)
                .WithErrorCode("User.Reason.InvalidLength");

        }
    }
}
