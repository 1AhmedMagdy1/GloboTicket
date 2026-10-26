using GloboTicket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Infrastructure.Persistence.Config
{
    public class EventCategoryConfigurations : IEntityTypeConfiguration<EventCategory>
    {
        private static readonly DateTimeOffset SeedCreatedAt =
            new(2026, 10, 20, 0, 0, 0, TimeSpan.Zero);

        public static readonly EventCategory[] All =
        [
            // Business & Professional
            new() { ID = 1, Name = "Conference", Description = "Professional or industry conferences", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 2, Name = "Seminar", Description = "Knowledge-sharing presentations and discussions", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 3, Name = "Workshop", Description = "Hands-on learning and practical sessions", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 4, Name = "Networking", Description = "Professional networking and relationship building", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 5, Name = "Corporate Event", Description = "Company meetings and corporate gatherings", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 6, Name = "Team Building", Description = "Activities that improve teamwork", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 7, Name = "Product Launch", Description = "Launches of products or services", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 8, Name = "Trade Show", Description = "Industry trade shows and exhibitions", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 9, Name = "Business Meeting", Description = "Internal or external business meetings", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 10, Name = "Awards Ceremony", Description = "Recognition and award presentations", IsSystem = true, CreatedAt = SeedCreatedAt },

    // Education & Technology
    new() { ID = 11, Name = "Training", Description = "Professional training and development", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 12, Name = "Webinar", Description = "Online presentations and educational sessions", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 13, Name = "Hackathon", Description = "Collaborative programming and innovation events", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 14, Name = "Academic Event", Description = "Academic and institutional gatherings", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 15, Name = "Career Fair", Description = "Recruitment and career opportunities", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 16, Name = "Scientific Event", Description = "Scientific conferences and research presentations", IsSystem = true, CreatedAt = SeedCreatedAt },

    // Social & Personal
    new() { ID = 17, Name = "Birthday", Description = "Birthday celebrations", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 18, Name = "Wedding", Description = "Wedding ceremonies and celebrations", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 19, Name = "Engagement", Description = "Engagement celebrations", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 20, Name = "Anniversary", Description = "Personal or organizational anniversaries", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 21, Name = "Private Party", Description = "Private parties and social gatherings", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 22, Name = "Family Gathering", Description = "Family occasions and reunions", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 23, Name = "Graduation", Description = "Graduation ceremonies and celebrations", IsSystem = true, CreatedAt = SeedCreatedAt },

    // Arts, Culture & Entertainment
    new() { ID = 24, Name = "Concert", Description = "Live music and musical performances", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 25, Name = "Festival", Description = "Cultural, seasonal, or community festivals", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 26, Name = "Art Exhibition", Description = "Art displays and gallery exhibitions", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 27, Name = "Theater & Performance", Description = "Plays, dance, and stage performances", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 28, Name = "Film Screening", Description = "Movie screenings and film events", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 29, Name = "Fashion Event", Description = "Fashion shows and industry events", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 30, Name = "Food & Drink Event", Description = "Food festivals, tastings, and culinary events", IsSystem = true, CreatedAt = SeedCreatedAt },

    // Community & Public
    new() { ID = 31, Name = "Community Event", Description = "Local community gatherings", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 32, Name = "Charity & Fundraising", Description = "Fundraising events and charitable activities", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 33, Name = "Religious Event", Description = "Religious gatherings and celebrations", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 34, Name = "Political & Civic Event", Description = "Public meetings and civic gatherings", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 35, Name = "Public Lecture", Description = "Public talks and expert presentations", IsSystem = true, CreatedAt = SeedCreatedAt },

    // Sports & Recreation
    new() { ID = 36, Name = "Sports Event", Description = "Sports competitions and sporting gatherings", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 37, Name = "Tournament", Description = "Organized competitive tournaments", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 38, Name = "Marathon & Race", Description = "Running races and endurance events", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 39, Name = "Outdoor Activity", Description = "Outdoor recreation and organized activities", IsSystem = true, CreatedAt = SeedCreatedAt },

    // Digital & Special Formats
    new() { ID = 40, Name = "Virtual Event", Description = "Events conducted entirely online", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 41, Name = "Hybrid Event", Description = "Events with both in-person and online participation", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 42, Name = "Exhibition", Description = "Public or commercial exhibitions", IsSystem = true, CreatedAt = SeedCreatedAt },
    new() { ID = 43, Name = "Other", Description = "Events that do not fit another category", IsSystem = true, CreatedAt = SeedCreatedAt }
        ];
        public void Configure(EntityTypeBuilder<EventCategory> builder)
        {
            builder.HasKey(ec => ec.ID);

            builder.Property(ec => ec.Name)
                .IsRequired()
                .HasMaxLength(100);



            builder.Property(ec => ec.Description)
                .HasMaxLength(500);

            builder.HasOne(ec => ec.Tenant)
                .WithMany()
                .HasForeignKey(ec => ec.TenantID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);


         

            builder.Property(ec => ec.IsSystem).IsRequired();
            builder.Property(ec => ec.IsDeleted).IsRequired();
            builder.Property(ec => ec.LogoID).IsRequired(false);


            builder.HasIndex(ec=>new {ec.TenantID, ec.Name }).IsUnique();

            builder.HasData(All);
        }
    }
}