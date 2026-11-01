using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GloboTicket.Application.Features.EventCategory.UpdateCategories
{
    internal class UpdateCategoryValidator:AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name)

                .NotEmpty()
                .WithErrorCode("CATEGORY_NAME_REQUIRED")
                .WithMessage("Category name is required.")
                .MaximumLength(100)
                .WithErrorCode("CATEGORY_NAME_MAX_LENGTH")
                .WithMessage("Category name must not exceed 100 characters.");
            RuleFor(x=>x.Description)
                .MaximumLength(500)
                .WithErrorCode("CATEGORY_DESCRIPTION_MAX_LENGTH")
                .WithMessage("Category description must not exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.LogoID)
                .MaximumLength(100)
                .WithErrorCode("CATEGORY_LOGO_MAX_LENGTH")
                .WithMessage("Category logo ID must not exceed 100 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.LogoID));
        }
    }
}
