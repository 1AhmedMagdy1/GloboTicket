using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Domain.Entities
{
    public class TicketNumberCounter
    {
        public int EventID { get; set; }
        public Event Event { get; set; } = null!;

        public long  LastNumber { get; set; }
    }
}
