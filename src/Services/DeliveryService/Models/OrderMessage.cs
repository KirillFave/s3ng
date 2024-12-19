using DeliveryService.Domain.External.Entities;
using DeliveryService.Enums;
using DeliveryService.Domain.External;

namespace DeliveryService.Models
{
    public class OrderMessage
    {
        public Guid OrderId { get; set; }
        public List<OrderItem> Items { get; set; }
        public int Total_Quantity { get; set; }
        public decimal Total_Price { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Shipping_Address { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
