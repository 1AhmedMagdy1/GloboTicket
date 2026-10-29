using GloboTicket.Application.Contracts.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.DTOS.UserManagement
{
    public class UserInfo
    {
        public string ID { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<SystemRolesEnum> Roles { get; set; } = new List<SystemRolesEnum>();
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsSuspended { get; set; }
    }
}
