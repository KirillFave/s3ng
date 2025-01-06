using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Database;

public class DatabaseContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    //public DbSet<OrderStatus> OrderStatuses { get; set; }
    //public DbSet<PaymentType> PaymentTypes { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(order => order.Guid);

            //entity
            //    .HasMany(order => order.Items)
            //    .WithOne()
            //    .HasForeignKey(orderItem => orderItem.OrderGuid);

            entity.Property(order => order.UserId)
                .IsRequired();
            entity.Property(order => order.Status)
                .HasConversion(status => 
                    status.ToString(), 
                    status => (OrderStatus) Enum.Parse(typeof(OrderStatus), status))
                .IsRequired();
            entity.Property(order => order.PaymentType)
                .HasConversion(paymentType =>
                    paymentType.ToString(),
                    paymentType => (PaymentType) Enum.Parse(typeof(PaymentType), paymentType))
                .IsRequired();
            entity.Property(order => order.ShipAddress)
                .HasMaxLength(255)
                .IsRequired();
            entity.Property(order => order.CreatedTimestamp)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(orderItem => orderItem.Guid);

            //entity.Property(orderItem => orderItem.Order)
            //    .IsRequired();
            entity.Property(orderItem => orderItem.OrderGuid)
                .IsRequired();
            entity.Property(orderItem => orderItem.ProductGuid)
                .IsRequired();
            entity.Property(orderItem => orderItem.PricePerUnit)
                .IsRequired();
            entity.Property(orderItem => orderItem.Count)
                .IsRequired();
            entity.Property(orderItem => orderItem.TotalPrice)
                .IsRequired();
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }
}
