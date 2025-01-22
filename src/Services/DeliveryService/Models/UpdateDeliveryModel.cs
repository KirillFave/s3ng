using DeliveryService.Enums;

namespace DeliveryService.Models
{
    public class UpdateDeliveryModel
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public required Guid UserId { get; set; }
        public Guid CourierId { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        public required int TotalQuantity { get; set; }
        public required decimal TotalPrice { get; set; }
        public required string ShippingAddress { get; set; }
        public required DateTime EstimatedDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }
    }
}
