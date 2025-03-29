using DeliveryService.Delivery.BusinessLogic.Enums;
using DeliveryService.Delivery.Domain.Entities.DeliveryEntities;

namespace DeliveryService.Delivery.BusinessLogic.Services.Contracts.Dto
{
    public class DeliveryResponseDto
    {
        public Guid Id { get; set; }
        public required Guid UserGuid { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }               
        public DateTime EstimatedDeliveryTime { get; set; }
    }
}
