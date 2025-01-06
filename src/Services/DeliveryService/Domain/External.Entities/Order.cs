using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Enums;

namespace DeliveryService.Domain.External.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<OrderItem> Items { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
