using DeliveryService.Delivery.DataAccess.Domain.Domain.Entities;
using DeliveryService.Delivery.DataAccess.Domain.External.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Delivery.DataAccess.Data
{    
    public class DeliveryDBContext : DbContext
    {
        public DeliveryDBContext(DbContextOptions<DeliveryDBContext> options) : base(options)
        {
        }
        /// <summary>
        /// Deliveries
        /// </summary>
        public DbSet<DataAccess.Domain.Domain.Entities.Delivery> Deliveries { get; set; } 
        /// <summary>
        /// Couriers
        /// </summary>
        public DbSet<Courier> Couriers { get; set; } 
        /// <summary>
        /// Orders
        /// </summary>
        public DbSet<Order> Orders { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Courier > Delivery (Многие к одному) 
            modelBuilder.Entity<DataAccess.Domain.Domain.Entities.Delivery>()
                .HasOne(u => u.Courier)
                .WithMany(c => c.Deliveries)
                .IsRequired();

            // Order > Delivery (Один к одному)
            modelBuilder.Entity<DataAccess.Domain.Domain.Entities.Delivery>()
                .HasOne(d => d.Order)
                .WithOne(o => o.Delivery)
                .HasForeignKey<DataAccess.Domain.Domain.Entities.Delivery>(d => d.OrderId)
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
