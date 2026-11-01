using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Application.Features.EventCategory.UpdateCategories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.EventCategory.DeleteCategory
{
    internal class DeleteCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<bool>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCommandHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }


        public async Task<Result<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var ExtingCategoryResult = await _categoryRepository.GetByIdAsync(request.ID, cancellationToken);
            if (ExtingCategoryResult.IsSuccess == false)
                return Result<bool>.NotFound(ExtingCategoryResult.Message,ExtingCategoryResult.Error);
            if (await CouldUserDeleteCategory(ExtingCategoryResult.Data!, cancellationToken) == false)
                return Result<bool>.Forbidden();
            var DeleteCategoryResult = await _categoryRepository.DeleteCategory(ExtingCategoryResult.Data!);
            if (DeleteCategoryResult.IsSuccess)
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            return DeleteCategoryResult;

        }
        public async Task<bool> CouldUserDeleteCategory(GloboTicket.Domain.Entities.EventCategory category, CancellationToken token)
        {
            var tenantID = _currentUserService.GetCurrentUserTenantID();
            var currentUserID = _currentUserService.getCurrentUserId();
            if (currentUserID is null)
                return false;
            var userRolesResult = await _userRepository.GetUserRolesAsync(currentUserID, token);

            if (!userRolesResult.IsSuccess)
                return false;
            var res = category.CouldUserDeleteCategory(tenantID, userRolesResult.Data!);
            return res;
        }
    }
}
