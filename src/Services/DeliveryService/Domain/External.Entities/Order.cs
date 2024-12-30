using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Enums;

namespace DeliveryService.Domain.External.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<OrderItem> Items { get; set; }
        public int Total_Quantity { get; set; }
        public decimal Total_Price { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string Shipping_Address { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
