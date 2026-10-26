using GloboTicket.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
namespace GloboTicket.Domain.Entities
{
    public class Ticket:AuditableEntity
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TicketNumber { get; set; } = string.Empty;
        public string userID { get; set; } = null!;
        public int EventID { get; set; }
        public Event Event { get; set; } = null!;
        public int TypeID { get; set; }
        public TicketType Type { get; set; } =null!;
        public TicketStatus Status { get; set; } = TicketStatus.Reserved;
        public DateTimeOffset ReservedAt { get; set; }
        public DateTimeOffset? CancelledAt { get; set; }

    }
}
