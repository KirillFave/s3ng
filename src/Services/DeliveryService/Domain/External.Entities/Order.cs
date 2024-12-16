using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Enums;

namespace DeliveryService.Domain.External.Entities
{
    public class Order : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public List<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShipAddress { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
