using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.UserManagement.GetUsers
{
    public class GetUsersValidator:AbstractValidator<GetUsersQuery>
    {
        public GetUsersValidator()
        {
            RuleFor(x => x.Search)
                .MaximumLength(100)
                .WithErrorCode("user.InvalidSearchTermLength")
                .WithMessage("Search term cannot exceed 100 characters.")
                .When(x => string.IsNullOrWhiteSpace(x.Search));

        
                
        }
    }
}
