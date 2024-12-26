using Microsoft.EntityFrameworkCore;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Domain.External.Entities;

namespace DeliveryService.Data
{
    public class DeliveryDBContext : DbContext
    {       
        public DeliveryDBContext(DbContextOptions<DeliveryDBContext> options) : base(options) 
        {
        }
        /// <summary>
        /// Deliveries
        /// </summary>
        public DbSet<Delivery> Deliveries { get; set; }
        /// <summary>
        /// Couriers
        /// </summary>
        public DbSet<Courier> Couriers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Delivery>()
                .HasMany(u => u.Couriers)
                .WithOne(c => c.Delivery)
                .IsRequired();            

            modelBuilder.Entity<Courier>().Property(c => c.Name).HasMaxLength(100);           
        }
    }
}
