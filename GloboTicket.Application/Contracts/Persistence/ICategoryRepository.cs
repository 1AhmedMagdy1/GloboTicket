using GloboTicket.Application.Common;
using GloboTicket.Application.DTOS.Categories;
using GloboTicket.Application.Features.EventCategory.CreateCategory;
using GloboTicket.Application.Features.EventCategory.DeleteCategory;
using GloboTicket.Application.Features.EventCategory.GetCategories;
using GloboTicket.Application.Features.EventCategory.UpdateCategories;
using GloboTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Contracts.Persistence
{
    public interface ICategoryRepository
    {
        public Task<Result<bool>> CreateCategoryAsync(CreateCategoryCommand command,int? TenantID);
        public Task<Result<Pagination<GetCategoriesDTO>>> GetCategoriesAsync(GetCategoriesQuery query,int? TenantID, CancellationToken cancellationToken);
        public Task<Result<bool>> UpdateCategory(EventCategory category, UpdateCategoryCommand request);
        public Task<Result<bool>> DeleteCategory(EventCategory category);
        public Task<Result<bool>> IsCategoryExists(int? TenantID, string Name);
        public Task<Result<EventCategory>> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
