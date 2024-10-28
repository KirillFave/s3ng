using DeliveryService.Models;
using Microsoft.EntityFrameworkCore;
using Nest;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Delivery> Deliveries { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseNpgsql("Host=localhost;Database=postgresDeliveryService;;Username=postgres;Password=myPassword");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("Delivery");
                entity.HasKey(p => p.Id).HasName("PK_Delivery");

                entity.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType("Guid").ValueGeneratedNever();

                entity.Property(p => p.Total_Price)
                .HasColumnName("Total_Price")
                .HasColumnType("decimal");

                entity.Property(p => p.Shipping_Address)
                .HasColumnName("Shipping_Address")
                .HasColumnType("string");

                //entity.Property(p => p.PaymentType)
                //.HasColumnName("PaymentType")
                //.HasColumnType("PaymentType");

                entity.Property(p => p.Order_Id)
                .HasColumnName("Order_Id")
                .HasColumnType("integer");

                entity.Property(p => p.Courer_Id)
                .HasColumnName("Courer_Id")
                .HasColumnType("integer");

                entity.Property(p => p.Delivery_Time)
                .HasColumnName("Delivery_Time")
                .HasColumnType("DateTime");
            });
        }       

    }
}
