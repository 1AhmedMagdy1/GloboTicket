using GloboTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Config
{
    public class TicketPricingTiersConfigurations : IEntityTypeConfiguration<TicketPricingTiers>
    {
        public void Configure(EntityTypeBuilder<TicketPricingTiers> builder)
        {
            builder.HasKey(t => t.ID);
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(t => t.BundleSize)
                .IsRequired();
            builder.Property(t => t.BundlePrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.HasOne(t => t.Event)
                .WithMany()
                .HasForeignKey(t => t.EventID)
                .OnDelete(DeleteBehavior.Cascade).IsRequired();

            builder.HasOne(t => t.TicketType)
                .WithMany()
                .HasForeignKey(t => t.TicketTypeID)
                .OnDelete(DeleteBehavior.Cascade).IsRequired();

            builder.HasOne(t => t.Tenant)
                .WithMany()
                .HasForeignKey(t => t.TenantID)
                .OnDelete(DeleteBehavior.NoAction).IsRequired();
        }
    }
}
