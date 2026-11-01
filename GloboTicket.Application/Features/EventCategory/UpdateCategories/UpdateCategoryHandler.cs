using FluentValidation;
using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
namespace GloboTicket.Application.Features.EventCategory.UpdateCategories
{
    public class UpdateCategoryHandler:IRequestHandler<UpdateCategoryCommand, Result<bool>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IValidator<UpdateCategoryCommand> _validator;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork unitOfWork;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService, IValidator<UpdateCategoryCommand> validator, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
            _validator = validator;
            this.unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task<Result<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {

            var InputValidationResult =await _validator.ValidateAsync(request, cancellationToken);
            if(!InputValidationResult.IsValid)
            {
                return ValidationErrorResult<bool>.Create(InputValidationResult.Errors.First());
              
            }
           
            var existingCategoryResult = await _categoryRepository.GetByIdAsync(request.ID, cancellationToken);

            if (!existingCategoryResult.IsSuccess)
           
                return Result<bool>.Failure(false, existingCategoryResult.Code, existingCategoryResult.Message,existingCategoryResult.Error);
       
            if(await CouldUserUpdateCategory(existingCategoryResult.Data!, cancellationToken)==false)
            {
                return Result<bool>.Forbidden();
            }

         
            var updateResult = await _categoryRepository.UpdateCategory(existingCategoryResult.Data!, request);
            if (updateResult.IsSuccess)
            {
               await unitOfWork.SaveChangesAsync(cancellationToken);
               
            }
            return updateResult;
        }
        public async Task<bool> CouldUserUpdateCategory(GloboTicket.Domain.Entities.EventCategory category, CancellationToken token)
        {
            var tenantID = _currentUserService.GetCurrentUserTenantID();
            var currentUserID = _currentUserService.getCurrentUserId();
            if (currentUserID is null)
                return false;
            var userRolesResult = await _userRepository.GetUserRolesAsync(currentUserID, token);

            if(!userRolesResult.IsSuccess)
                return false;
            var res= category.CouldUserUpdateCategory(tenantID, userRolesResult.Data!);
            return res;
        }

    }
    
}
