using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Persistence;
using GloboTicket.Application.DTOS.Categories;
using GloboTicket.Application.Features.EventCategory.CreateCategory;
using GloboTicket.Application.Features.EventCategory.DeleteCategory;
using GloboTicket.Application.Features.EventCategory.GetCategories;
using GloboTicket.Application.Features.EventCategory.UpdateCategories;
using GloboTicket.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace GloboTicket.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<bool>> CreateCategoryAsync(CreateCategoryCommand command,int? TenantID)
        {
           
            var category = new EventCategory
            {
                Name = command.Name,
                Description = command.Description,
                LogoID = command.LogoID,
                CreatedAt = DateTimeOffset.UtcNow,
                IsSystem=TenantID == null ? true : false,
                TenantID = TenantID,
                
            };
            _dbContext.EventCategories.Add(category);
            return Result<bool>.Success(true, ResultType.Created,"Category created successfully");
        }

 

        public Task<Result<bool>> DeleteCategory(EventCategory category)
        {
            _dbContext.EventCategories.Remove(category);
            return Task.FromResult(Result<bool>.Success(true, ResultType.NoContent, "Category deleted successfully"));
        }

        public async Task<Result<EventCategory>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var category =await _dbContext.EventCategories.FirstOrDefaultAsync(c => c.ID == id, cancellationToken);
            if(category is null)
            {
                var error = new ErrorResult("CATEGORY_NOT_FOUND", "The specified category does not exist.");
              
                return  Result<EventCategory>.NotFound(error.ErrorMessage, error);
            }
            return Result<EventCategory>.Success(category, ResultType.OK);
        }

        public async Task<Result<Pagination<GetCategoriesDTO>>> GetCategoriesAsync(GetCategoriesQuery query,int? TenantID, CancellationToken cancellationToken)
        {
            var categoriesQuery = _dbContext.EventCategories.AsQueryable();
            if(query.FilterType == CategoriesFilterType.Tenant)
            {
                categoriesQuery = categoriesQuery.Where(c => c.TenantID == TenantID);
            }
            else if (query.FilterType == CategoriesFilterType.System)
            {
                categoriesQuery = categoriesQuery.Where(c => c.IsSystem);
            }
            if (!string.IsNullOrWhiteSpace(query.Search)){
                query.Search = query.Search.Trim();
                categoriesQuery = categoriesQuery.Where(c => c.Name.StartsWith(query.Search) || (c.Description!=null&&c.Description.Contains(query.Search)));
            }

            if(query.sortDirection==SortDirection.Asc)
                categoriesQuery = categoriesQuery
                    .OrderBy(c => c.CreatedAt)
                    .ThenBy(c => c.ID);
                    
            
            else
                categoriesQuery = categoriesQuery
                    .OrderByDescending(c => c.CreatedAt)
                    .ThenBy(c => c.ID);
           
            var totalCount = categoriesQuery.Count();
            var categories =await categoriesQuery
                .Skip((query.Page - 1) * query.Size)
                .Take(query.Size)
                .Select(c => new GetCategoriesDTO
                {
                    ID = c.ID,
                    Name = c.Name,
                    Description = c.Description,
                    TenantID = c.TenantID,
                    ImageID = c.LogoID,
                    CreatedAt = c.CreatedAt
                })
                .ToListAsync();
            var pagination = new Pagination<GetCategoriesDTO>(query.Page, query.Size, categories, totalCount);
        
          
         
            return Result<Pagination<GetCategoriesDTO>>.Success(pagination, ResultType.OK);

        }

        public async Task<Result<bool>> IsCategoryExists(int? TenantID, string Name)
        {
            var categoryExists =await _dbContext.EventCategories.AnyAsync(c => c.Name == Name && c.TenantID == TenantID);
            if(categoryExists)
                return Result<bool>.Success(true, ResultType.OK, "Category already exists");
            return Result<bool>.NotFound();
        }

        public async Task<Result<bool>> UpdateCategory(EventCategory category, UpdateCategoryCommand request)
        {
            
            category.Name = request.Name;
            category.Description = request.Description;
            category.LogoID = request.LogoID;
            return  Result<bool>.Success(true, ResultType.NoContent);
        }
    }
}
