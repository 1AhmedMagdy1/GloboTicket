using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<SuspensionLog> SuspensionLogs { get; set; }

    }
    public class PhoneNumbers
    {
        public int Id { get; set; }
        public string Number { get; set; } = string.Empty;
      
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
    public class SuspensionLog
    {
        public int Id { get; set; }
        public string AffectedUserId { get; set; } = string.Empty;
        public ApplicationUser AffectedUser { get; set; } = null!;

        public string SuspensionActorID { get; set; } = string.Empty;
        public ApplicationUser SuspensionActor { get; set; } = null!;

        public string UnsuspensionActorID { get; set; } = string.Empty;
        public ApplicationUser UnsuspensionActor { get; set; } = null!;
        public DateTime SuspendedAt { get; set; }
        public DateTime? UnsuspendedAt { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
