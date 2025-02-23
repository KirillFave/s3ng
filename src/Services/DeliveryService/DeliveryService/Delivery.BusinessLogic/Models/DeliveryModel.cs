using DeliveryService.Delivery.BusinessLogic.Enums;

namespace DeliveryService.Delivery.BusinessLogic.Models
{
    public class DeliveryModel
    {
        public Guid Id { get; set; }
        //public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatus orderStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public required int TotalQuantity { get; set; }
        public required decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }
        public Guid CourierId { get; set; }
        public required DateTime EstimatedDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }
    }
}
