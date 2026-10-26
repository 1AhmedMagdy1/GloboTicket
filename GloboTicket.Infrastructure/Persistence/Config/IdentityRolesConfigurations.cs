using GloboTicket.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Config
{
    public class IdentityRolesConfigurations : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(GetRoles());
        }

   
    public IEnumerable<IdentityRole> GetRoles()
      {
            var roles = new List<IdentityRole>();
            


         var r1 = new IdentityRole
         {
             Id = "eb708a75 - d290 - 4195 - 9058 - b6d24bf26626",
             Name = Roles.SystemAdmin,
             NormalizedName = Roles.SystemAdmin.ToUpper(),
             ConcurrencyStamp= "ba6d315f-ce5c-4d4e-a713-2f8683911540"
         };
         var r2 = new IdentityRole
         {
             Id = "382601e0 - 8d06 - 4bc6 - 9a04 - c9d5b169aa89",
             Name = Roles.TenantAdmin,
             NormalizedName = "CR-" + Roles.TenantAdmin.ToUpper(),
             ConcurrencyStamp= "162705e6-7360-4281-a427-e7137ae9ef64"

         };
         var r3 = new IdentityRole
         {
             Id = "577fab6a - 4196 - 4b7a - b8dd - 6a8a08a78a1c",
             Name = Roles.CustomerUser,
             NormalizedName = Roles.CustomerUser.ToUpper(),
             ConcurrencyStamp= "e8d758fd-3c82-4bdd-add6-828b31fec748"
         };
            roles.Add(r1);
            roles.Add(r2);
            roles.Add(r3);

            return roles;
        }
        

        
    }
   
}
