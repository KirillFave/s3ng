using DeliveryService.Domain.Domain.Entities;

namespace DeliveryService.Domain.External.Entities
{
    public class OrderItem : IEntity<Guid>
    {       
        public Guid Id { get; set; }
        public Guid ProductGuid { get; }
        public Decimal PricePerUnit { get; private set; }
        public int Qnty { get; }

        public Order ? Order { get; set; }
        public Decimal TotalPrice { get; } 
       
    }
}
