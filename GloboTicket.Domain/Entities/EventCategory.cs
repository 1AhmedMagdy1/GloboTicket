using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Domain.Entities
{
    public class EventCategory:AuditableEntity
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? TenantID { get; set; }
        public Tenant? Tenant { get; set; }
        public bool IsSystem { get; set; }
        public bool IsDeleted { get; set; }
        public string? LogoID { get; set; }

    }
}
