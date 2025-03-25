using DeliveryService.Domain;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Domain.External.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Delivery.Domain.Entities.External.Entities
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        { }

        public DbSet<Order> Orders { get; set; }
    }
}
