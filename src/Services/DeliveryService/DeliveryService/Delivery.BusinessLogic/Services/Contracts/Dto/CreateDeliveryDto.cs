using DeliveryService.Delivery.BusinessLogic.Enums;
using System.Diagnostics.Metrics;
using DeliveryService.Delivery.DataAccess.Domain.Domain.Entities;

namespace DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto
{
    public class CreateDeliveryDto
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
        public required Guid CourierId { get; set; }
        public Courier? Courier { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }
    }
}
