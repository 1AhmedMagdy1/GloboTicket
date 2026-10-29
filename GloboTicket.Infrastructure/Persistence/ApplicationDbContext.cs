using GloboTicket.Domain.Entities;
using GloboTicket.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        public virtual DbSet<Event> Events => Set<Event>();
        public virtual DbSet<TicketType> TicketTypes => Set<TicketType>();
        public virtual DbSet<TicketPricingTiers> TicketPricingTiers => Set<TicketPricingTiers>();
        public virtual DbSet<Ticket> Tickets =>Set<Ticket>();
        public virtual DbSet<Tenant> Tenants => Set<Tenant>();
        public virtual DbSet<EventCategory> EventCategories => Set<EventCategory>();
        public virtual DbSet<PhoneNumbers> PhoneNumbers => Set<PhoneNumbers>();
        public virtual DbSet<SuspensionLog>SuspensionLogs { get; set; }

    }
}