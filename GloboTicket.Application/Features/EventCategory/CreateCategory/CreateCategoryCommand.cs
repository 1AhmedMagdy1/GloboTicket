using GloboTicket.Application.Common;
using GloboTicket.Application.Contracts.Identity;
using GloboTicket.Application.Contracts.Identity.Authorization;
using GloboTicket.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GloboTicket.Application.Features.EventCategory.CreateCategory
{
    public class CreateCategoryCommand:IRequest<Result<bool>>,IRequireAuthorization
    {
    
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        
        public string? LogoID { get; set; }
      

        public IReadOnlyCollection<string> RequiredRoles => [SystemRolesEnum.SystemAdmin.ToString(),SystemRolesEnum.TenantAdmin.ToString()];
    }
}
