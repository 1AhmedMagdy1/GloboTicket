using GloboTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Config
{
    public class TenantConfigurations : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(t => t.ID);
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(t => t.Description)
                .HasMaxLength(500);

            builder.Property(t=>t.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(t => t.ContactPhone)
                .HasMaxLength(15)
                .IsRequired();
            builder.Property(t => t.ContactEmail)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(t => t.Name)
                .IsUnique();

            builder.HasIndex(t=>t.ContactEmail)
                .IsUnique();
            builder.HasIndex(t => t.ContactPhone)
                .IsUnique();

        }
    }
}
