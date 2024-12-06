using Microsoft.EntityFrameworkCore;
using DeliveryService.Domain.Domain.Entities;

namespace DeliveryService.Data
{
    public class DeliveryDBContext : DbContext
    {  
       public DbSet<DeliveryModel> Deliveries {  get; set; }
        public DeliveryDBContext(DbContextOptions<DeliveryDBContext> options) : base(options) 
        {
        }
    }
}
