using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Domain.Entities
{
    public class TicketPricingTiers:AuditableEntity
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BundleSize { get; set; }
        public decimal BundlePrice { get; set; }
        public int? QuantityLimit { get; set; }
        public DateTimeOffset?  SalesStartsAt{ get; set; }
        public DateTimeOffset?  SalesEndsAt{ get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; } = null!;
        public int TicketTypeID { get; set; }
        public TicketType TicketType { get; set; } = null!;
        public int TenantID { get; set; }
        public Tenant Tenant { get; set; } = null!;
     
    }
}
