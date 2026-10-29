using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.GetUserByID
{
    public class GetUserByIDValidator:AbstractValidator<GetUserByIDQuery>
    {
        public GetUserByIDValidator()
        {
            RuleFor(v => v.ID)
                .NotEmpty()
                .WithErrorCode("user.ID.Required")
                .WithMessage("User ID is Required")
                .MaximumLength(50)
                .WithErrorCode("user.ID.InvalidLength")
                .WithMessage("User ID must not exceed 50 characters.")
        }
    }
}
