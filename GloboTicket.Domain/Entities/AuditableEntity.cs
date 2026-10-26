using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Domain.Entities
{
    public class AuditableEntity
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
