using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.DTOS.Categories
{
    public class UpdateCategoryDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public string? LogoID { get; set; }
    }
}
