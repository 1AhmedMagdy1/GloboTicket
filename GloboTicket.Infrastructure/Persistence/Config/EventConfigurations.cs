using GloboTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Config
{
    internal class EventConfigurations : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(ev => ev.ID);
               builder.Property(ev => ev.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ev => ev.Description)
                .IsRequired()
                .HasMaxLength(500);

          
            builder.HasOne(ev => ev.Tenant)
                .WithMany()
                .HasForeignKey(ev => ev.TenantID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.HasOne(e=>e.Category)
                .WithMany()
                .HasForeignKey(e => e.CategoryID)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
        }
    }
}
