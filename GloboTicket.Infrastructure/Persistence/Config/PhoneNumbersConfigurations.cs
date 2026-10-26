using GloboTicket.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Config
{
    public class PhoneNumbersConfigurations : IEntityTypeConfiguration<PhoneNumbers>
    {
        public void Configure(EntityTypeBuilder<PhoneNumbers> builder)
        {
            builder.HasKey(ph => ph.Id);

            builder.HasOne(ph => ph.ApplicationUser)
                   .WithMany()
                   .HasForeignKey(ph => ph.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ph => ph.Number)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.HasIndex(ph => new {ph.ApplicationUserId, ph.Number})
                   .IsUnique();
        }
    }
}
