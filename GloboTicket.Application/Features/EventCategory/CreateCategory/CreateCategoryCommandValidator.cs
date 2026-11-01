using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.EventCategory.CreateCategory
{
    public class CreateCategoryCommandValidator:AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithErrorCode("CategoryNameRequired")
                .WithMessage("Category name is required.")
                .WithErrorCode("CategoryNameMaxLength")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithErrorCode("CategoryDescriptionMaxLength")
                .WithMessage("Category description must not exceed 500 characters.");

        }
    }
}
