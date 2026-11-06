using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Identity
{
    public class RefreshToken
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public string TokenHash { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
        public DateTimeOffset? RevokedAt { get; set; }
    }
}
