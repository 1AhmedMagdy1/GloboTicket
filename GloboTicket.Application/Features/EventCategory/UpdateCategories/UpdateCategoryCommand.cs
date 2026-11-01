using GloboTicket.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Features.EventCategory.UpdateCategories
{
    public class UpdateCategoryCommand:IRequest<Result<bool>>
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
      
        public string? LogoID { get; set; }
    
    
    }
}
