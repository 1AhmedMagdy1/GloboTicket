using GloboTicket.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using GloboTicket.Application.DTOS.Categories;
namespace GloboTicket.Application.Features.EventCategory.GetCategories
{
    public class GetCategoriesQuery:IRequest<Result<Pagination<GetCategoriesDTO>>>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public CategoriesFilterType FilterType { get; set; }
        public string Search { get; set; } = string.Empty;//search by name or description
        public SortDirection sortDirection { get; set; } = SortDirection.Desc;
    }
    public enum CategoriesFilterType
    {
        Tenant,
        System,
        All
    }
}
