using DeliveryService.Delivery.BusinessLogic.Enums;
using DeliveryService.Delivery.DataAccess.Domain.External.Entities;

namespace DeliveryService.Kafka.Messages
{
    public class DeliveryMessage
    {
        public Guid DeliveryId { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public Guid OrderId { get; set; }
        //public Guid UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public string ShippingAddress { get; set; } = null!;
        public Guid CourierId { get; set; }
        public required DateTime EstimatedDeliveryTime { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public List<OrderItem>? Items { get; set; }
    }
}
