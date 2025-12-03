using FloorPlanApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FloorPlanApplication.Data
{
    public class AppDBContext : IdentityDbContext<User>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Service>()
                .HasOne(s => s.Employee)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            builder.Entity<Service>()
                .HasOne(s => s.Client)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(true);

            builder.Entity<Service>()
                .HasOne(s => s.Company)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceLog> ServicesLogs { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
    }
}
