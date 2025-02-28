using DeliveryService.Delivery.BusinessLogic.Enums;
using DeliveryService.Delivery.DataAccess.Domain.Domain.Entities;

namespace DeliveryService.Delivery.BusinessLogic.Models
{
    public class CreateDeliveryModel
    {
        public Guid Id { get; set; }
        public required Guid UserGuid { get; set; }
        public Guid OrderId { get; set; }
        public OrderState OrderStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }
        public Guid? CourierId { get; set; }
        public Courier? Courier { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }
    }
}
