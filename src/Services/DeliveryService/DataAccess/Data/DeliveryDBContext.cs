using Microsoft.EntityFrameworkCore;
using DeliveryService.DataAccess.Domain.Domain.Entities;
using DeliveryService.DataAccess.Domain.External.Entities;

namespace DeliveryService.DataAccess.Data
{
    public class DeliveryDBContext : DbContext
    {
        public DeliveryDBContext(DbContextOptions<DeliveryDBContext> options) : base(options)
        {
        }
        /// <summary>
        /// Deliveries
        /// </summary>
        public DbSet<Delivery> Deliveries { get; set; } = null!;
        /// <summary>
        /// Couriers
        /// </summary>
        public DbSet<Courier> Couriers { get; set; } = null!;
        /// <summary>
        /// Orders
        /// </summary>
        public DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Courier > Delivery (Многие к одному) 
            modelBuilder.Entity<Delivery>()
                .HasOne(u => u.Courier)
                .WithMany(c => c.Deliveries)
                .IsRequired();

            // Order > Delivery (Один к одному)
            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Order)
            .WithOne(o => o.Delivery)
            .HasForeignKey<Delivery>(d => d.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Courier>().Property(c => c.Name).HasMaxLength(100);
            modelBuilder.Entity<Order>().Property(c => c.ShippingAddress).HasMaxLength(200);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
