using DeliveryService.Domain.External.Entities;
using DeliveryService.Enums;
using DeliveryService.Domain.External;

namespace DeliveryService.Models
{
    public class OrderMessage
    {
        public Guid OrderId { get; set; }
        public List<OrderItem> Items { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentType PaymentType { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
