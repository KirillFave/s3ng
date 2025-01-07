using System.ComponentModel.DataAnnotations;
using DeliveryService.Enums;

namespace DeliveryService.Services.Services.Contracts.DTO
{
    public class UpdateDeliveryDTO
    {
        public required Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public PaymentType PaymentType { get; set; }
        public required decimal TotalPrice { get; set; }
        public required string ShippingAddress { get; set; }
        public required DateTime EstimatedDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }
    }
}
