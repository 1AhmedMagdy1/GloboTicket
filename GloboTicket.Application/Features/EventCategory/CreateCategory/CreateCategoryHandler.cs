using FluentValidation;
using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Application.ExtensionMethods;
using GloboTicket.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.EventCategory.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Result<bool>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IValidator<CreateCategoryCommand> _validator;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork unitOfWork;
        public CreateCategoryHandler(ICategoryRepository categoryRepository, IValidator<CreateCategoryCommand> validator, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
            this.unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<bool>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var RequestValidationResult=await _validator.ValidateAsync(request);
            if(!RequestValidationResult.IsValid)
            {
                var error = ValidationErrorResult<bool>.Create(RequestValidationResult.Errors.First());
                return error;
            }

            var categoryExists = await _categoryRepository.IsCategoryExists(_currentUserService.GetCurrentUserTenantID(), request.Name);
            if (categoryExists.IsSuccess)
                return Result<bool>.Failure(false, ResultType.Conflict, "Category already exists", new ErrorResult("CategoryExists", "Category already exists"));


            var result = await _categoryRepository.CreateCategory(request,_currentUserService.GetCurrentUserTenantID());
            if(result.IsSuccess)
            {
             
               await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return result;
        }

    }
}
