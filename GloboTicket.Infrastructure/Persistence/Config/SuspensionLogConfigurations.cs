using GloboTicket.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Config
{
    internal class SuspensionLogConfigurations : IEntityTypeConfiguration<SuspensionLog>
    {
        public void Configure(EntityTypeBuilder<SuspensionLog> builder)
        {
            builder.HasKey(sl => sl.Id);
            builder.HasOne(sl=>sl.AffectedUser)
                .WithMany()
                .HasForeignKey(sl=>sl.AffectedUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sl=>sl.SuspensionActor)
                .WithMany()
                .HasForeignKey(sl=>sl.SuspensionActorID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sl=>sl.UnsuspensionActor)
                .WithMany()
                .HasForeignKey(sl=>sl.UnsuspensionActorID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
