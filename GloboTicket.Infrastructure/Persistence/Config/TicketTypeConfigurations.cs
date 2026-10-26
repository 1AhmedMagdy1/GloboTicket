using GloboTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Config
{
    public class TicketTypeConfigurations : IEntityTypeConfiguration<TicketType>
    {
        public void Configure(EntityTypeBuilder<TicketType> builder)
        {
            builder.HasKey(tp => tp.ID);

            builder.Property(tp => tp.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(tp => tp.Tenant)
                .WithMany()
                .HasForeignKey(tp => tp.TenantID)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder.HasIndex(tp => new { tp.TenantID, tp.Name }).IsUnique();
            



        }
    }
}
