using GloboTicket.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace GloboTicket.Domain.Entities
{
    public class Tenant:AuditableEntity
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? LogoID { get; set; }
        public string ContactPhone { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public TenantStatus Status { get; set; }
        public ICollection<Event> Events { get; set; } = new List<Event>();

    }
}
