using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Identity
{
   

    public static class Roles
    {
        public  const string SystemAdmin = "SystemAdmin";
        public const string TenantAdmin = "TenantAdmin";
       public const string CustomerUser= "CustomerUser";

        public static string[] SystemRoles => new string[] {

       SystemAdmin,
       TenantAdmin,
       CustomerUser
    };

    }
}
