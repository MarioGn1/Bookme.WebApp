using Bookme.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookme.Data
{
    public class BookmeDbContext : IdentityDbContext<ApplicationUser>
    {
        public BookmeDbContext(DbContextOptions<BookmeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingConfiguration> BookingConfigurations { get; set; }
        public DbSet<BreakTemplate> BreakTemplates { get; set; }
        public DbSet<Business> BusinessInfos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ConfirmationType> ConfirmationTypes { get; set; }
        public DbSet<OfferedService> OfferedServices { get; set; }
        public DbSet<Raiting> Raitings { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ServiceVisitation> ServiceVisitations { get; set; }
        public DbSet<VisitationType> VisitationTypes { get; set; }
        public DbSet<WeeklySchedule> WeeklySchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(x =>
            {
                x.HasOne(y => y.Business)
                .WithOne(y => y.User)
                .HasForeignKey<Business>(y => y.UserId);
            });

            builder.Entity<Booking>(x =>
            {
                x.HasOne(y => y.Client)
                .WithMany(y => y.Bookings)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<BookingConfiguration>(x =>
            {
                x.HasOne(y => y.WeeklySchedule)
                .WithOne(y => y.BookingConfiguration)
                .HasForeignKey<WeeklySchedule>(y => y.BookingConfigurationId);

                x.HasOne(y => y.Business)
                .WithOne(y => y.BookingConfiguration)
                .HasForeignKey<Business>(y => y.BookingConfigurationId);
            });

            builder.Entity<Raiting>(x =>
            {
                x.HasKey(x => new { x.VoterId, x.CommentId });
            });

            builder.Entity<Comment>(x =>
            {
                x.HasOne(y => y.BusinessInfo)
                .WithMany(y => y.Comments)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<ServiceVisitation>(x =>
            {
                x.HasKey(x => new { x.OfferedServiceId, x.VisitationTypeId });
            });

            base.OnModelCreating(builder);
        }
    }
}
