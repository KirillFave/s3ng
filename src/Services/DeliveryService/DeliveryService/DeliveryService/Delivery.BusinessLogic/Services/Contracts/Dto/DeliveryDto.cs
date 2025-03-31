using DeliveryService.Delivery.BusinessLogic.Enums;

namespace DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto
{
    public class DeliveryDto
    {
        public required Guid Id { get; set; }
        public required Guid UserGuid { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public Guid OrderId { get; set; }
        public required OrderStatus OrderStatus { get; set; }
        public required int TotalQuantity { get; set; }
        public required decimal TotalPrice { get; set; }
        public required string ShippingAddress { get; set; }
        public required DateTime EstimatedDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }
    }
}
