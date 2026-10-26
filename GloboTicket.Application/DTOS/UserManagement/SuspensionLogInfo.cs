using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.DTOS.UserManagement
{
    public class SuspensionLogInfo
    {

        public int Id { get; set; }
        public string AffectedUserEmail { get; set; } = string.Empty;
        

        public string SuspensionActorEmail { get; set; } = string.Empty;
     

        public string UnsuspensionActorEmail { get; set; } = string.Empty;
       
        public DateTime SuspendedAt { get; set; }
        public DateTime? UnsuspendedAt { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
