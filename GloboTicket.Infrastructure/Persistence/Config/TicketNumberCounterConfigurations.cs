using GloboTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Config
{
    public class TicketNumberCounterConfigurations : IEntityTypeConfiguration<TicketNumberCounter>
    {
        public void Configure(EntityTypeBuilder<TicketNumberCounter> builder)
        {
            builder.HasKey(t => t.EventID );

            builder.Property(t => t.LastNumber)
                .IsRequired();
               
            builder.HasOne(t => t.Event)
                .WithOne()
                .HasForeignKey<TicketNumberCounter>(t => t.EventID)
                .OnDelete(DeleteBehavior.Cascade).IsRequired();

        }
    }
}
