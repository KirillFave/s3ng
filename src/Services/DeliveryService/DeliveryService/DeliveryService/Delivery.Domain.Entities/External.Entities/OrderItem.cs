using DeliveryService.Delivery.Domain.Entities.DeliveryEntities;

namespace DeliveryService.Domain.External.Entities
{
    public class OrderItem : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid ProductGuid { get; }
        public decimal PricePerUnit { get; private set; }
        public int Qnty { get; }

        public Order? Order { get; set; }
        public decimal TotalPrice { get; }
    }
}
