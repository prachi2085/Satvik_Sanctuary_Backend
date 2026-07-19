using Microsoft.EntityFrameworkCore;
using HealthWellness.Models;

namespace HealthWellness.Data
{
    public class WellnessDbContext : DbContext
    {
        public WellnessDbContext(DbContextOptions<WellnessDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<HealthForm> HealthForms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<SessionRegistration> SessionRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.HealthForms)
                .WithOne(h => h.User)
                .HasForeignKey(h => h.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ChatMessages)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        }
    }

}

