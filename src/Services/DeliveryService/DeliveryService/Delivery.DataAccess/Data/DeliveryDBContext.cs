using DeliveryService.Domain.External.Entities;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Delivery.Domain.Entities.DeliveryEntities;

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
        public DbSet<DeliveryService.Delivery.Domain.Entities.DeliveryEntities.Delivery> Deliveries { get; set; }
         
        /// <summary>
        /// Orders
        /// </summary>
        public DbSet<Order> Orders { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            // Order > Delivery (Один к одному)
            modelBuilder.Entity<DeliveryService.Delivery.Domain.Entities.DeliveryEntities.Delivery>()
                .HasOne(d => d.Order)
                .WithOne(o => o.Delivery)
                .HasForeignKey<DeliveryService.Delivery.Domain.Entities.DeliveryEntities.Delivery>(d => d.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Order>().Property(c => c.ShippingAddress).HasMaxLength(200);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
