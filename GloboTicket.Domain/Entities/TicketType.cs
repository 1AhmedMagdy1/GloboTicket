using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Domain.Entities
{
    public class TicketType
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? TenantID { get; set; }
        public Tenant? Tenant { get; set; }
        public bool IsSystem { get; set; }
    }
}
