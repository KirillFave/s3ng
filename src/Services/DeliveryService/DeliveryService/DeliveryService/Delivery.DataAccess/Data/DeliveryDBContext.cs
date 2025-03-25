using DeliveryService.Domain.External.Entities;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Delivery.Domain.Entities.DeliveryEntities;
using Confluent.Kafka;

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
        public DbSet<DeliveryService.Delivery.Domain.Entities.DeliveryEntities.Delivery> Deliveries { get; set; } = null!;
         
        /// <summary>
        /// Orders
        /// </summary>
        public DbSet<Order> Orders { get; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Order > Delivery (Один к одному)
            modelBuilder.Entity<DeliveryService.Delivery.Domain.Entities.DeliveryEntities.Delivery>()
                .HasOne(d => d.Order)
                .WithOne(o => o.Delivery)
              //  .HasForeignKey<DeliveryService.Delivery.Domain.Entities.DeliveryEntities.Delivery>(d => d.OrderId)                
                .OnDelete(DeleteBehavior.Restrict);     //Cascade — зависимые сущности должны быть удалены. 
                                                        //Restrict — зависимые сущности не затрагиваются. 
                                                        //SetNull — значения внешних ключей в зависимых строках должны обновляться до значения NULL.
            //modelBuilder.Entity<Order>().HasKey(i => i.Id);


            modelBuilder.Entity<Order>().Property(c => c.ShippingAddress).HasMaxLength(200);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
