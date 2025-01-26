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
        /// Couriers
        /// </summary>
        public DbSet<Courier> Couriers { get; set; } = null!;
        /// <summary>
        /// Deliveries
        /// </summary>
        public DbSet<Delivery> Deliveries { get; set; } = null!;
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Courier>()
                .HasMany(u => u.Deliveries)
                .WithOne(c => c.Courier)
                .IsRequired();
           
            modelBuilder.Entity<Delivery>().Property(c => c.Id).HasMaxLength(200);
            modelBuilder.Entity<Courier>().Property(c => c.Id).HasMaxLength(100);
        }
    }
}
