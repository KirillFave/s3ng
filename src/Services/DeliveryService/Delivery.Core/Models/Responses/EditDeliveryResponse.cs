using DeliveryService.Delivery.BusinessLogic.Enums;

namespace DeliveryService.Delivery.Core.Models.Responses
{
    public class EditDeliveryResponse
    {
        public Guid Id { get; set; }
        public required Guid UserGuid { get; set; }
        public Guid OrderId { get; set; }        
        public DeliveryStatus DeliveryStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        public required int TotalQuantity { get; set; }
        public required decimal TotalPrice { get; set; }
        public required string ShippingAddress { get; set; }
        public required DateTime EstimatedDeliveryTime { get; set; }
    }
}
