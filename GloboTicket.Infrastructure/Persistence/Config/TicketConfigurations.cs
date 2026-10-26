using GloboTicket.Domain.Entities;
using GloboTicket.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Config
{
    public class TicketConfigurations : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.ID);
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(t => t.TicketNumber)
                .IsRequired()
                .HasMaxLength(210);

            builder.Property(t => t.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(t => t.Event)
               .WithMany()
               .HasForeignKey(t => t.EventID)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Type)
                .WithMany()
                .HasForeignKey(t => t.TypeID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(t => t.userID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(t => t.TicketNumber)
                .IsUnique();

            builder.ToTable("CK_ReservedAt_ReservedAt_Before_CancelledAt", "[CancelledAt] IS NULL OR [ReservedAt] <= [CancelledAt]");
        }
    }
}
