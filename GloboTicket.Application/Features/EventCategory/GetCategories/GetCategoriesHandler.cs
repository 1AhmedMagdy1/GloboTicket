using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.Contracts.Services;
using GloboTicket.Application.DTOS.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.EventCategory.GetCategories
{
    internal class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, Result<Pagination<GetCategoriesDTO>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUserService _currentUserService;
        public GetCategoriesHandler(ICategoryRepository categoryRepository, ICurrentUserService currentUserService)
        {
            _categoryRepository = categoryRepository;
            _currentUserService = currentUserService;
        }

        public Task<Result<Pagination<GetCategoriesDTO>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = _categoryRepository.GetCategoriesAsync(request,_currentUserService.GetCurrentUserTenantID(), cancellationToken);
            return result;
        }
    }
}
