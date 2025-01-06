using DeliveryService.Enums;

namespace DeliveryService.Models
{
    public class UpdateDeliveryModel
    {       
        public Guid OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }
        public Guid Courier_Id { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
    }
}
