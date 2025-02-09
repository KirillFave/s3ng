using DeliveryService.Domain.External;
using DeliveryService.BL.Enums;
using DeliveryService.DataAccess.Domain.External.Entities;

namespace DeliveryService.DataAccess.Models
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
