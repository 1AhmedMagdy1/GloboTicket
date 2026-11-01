using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.DTOS.Categories
{
    public class GetCategoriesDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? TenantID { get; set; }
        public string? ImageID { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
