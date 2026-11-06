using GloboTicket.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Config
{
    public class RefreshTokenConfigurations : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.ID);
            builder.HasOne(r => r.ApplicationUser)
                .WithMany()
                .HasForeignKey(r => r.UserID);

            builder.
               HasIndex(r => r.UserID)
               .IsUnique()
               .HasFilter("\"RevokedAt\" is Null");
        }
    }
}
