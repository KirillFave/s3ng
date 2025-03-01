using DeliveryService.Delivery.BusinessLogic.Enums;
using DeliveryService.Delivery.DataAccess.Domain.External.Entities;

namespace DeliveryService.Kafka.Messages
{
    public class OrderMessage
    {
        public Guid OrderId { get; set; }
        public List<OrderItem> Items { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderState Status { get; set; }
        public PaymentType PaymentType { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
