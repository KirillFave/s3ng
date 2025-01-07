using System.ComponentModel.DataAnnotations;
using DeliveryService.Enums;

namespace DeliveryService.Services.Services.Contracts.DTO
{
    public class DeliveryDTO
    {
        public required Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public required OrderStatus OrderStatus { get; set; }
        public required int TotalQuantity { get; set; }
        public required decimal TotalPrice { get; set; }
        public required string ShippingAddress { get; set; }
        public required DateTime EstimatedDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }
    }
}


