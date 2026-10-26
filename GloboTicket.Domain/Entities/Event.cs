using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Domain.Entities
{
    public class Event:AuditableEntity
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryID { get; set; }
        public EventCategory Category { get; set; } = null!;
        public string? EventLogoID { get; set; }
        public DateTimeOffset StartsAt { get; set; }
        public DateTimeOffset? LastsFor { get; set; }
        public int TenantID { get; set; }
        public Tenant Tenant { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
