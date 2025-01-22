using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.External.Entities;
using DeliveryService.Enums;

namespace DeliveryService.Services.Services.Contracts.DTO
{
    public class CreateDeliveryDTO
    {
        public Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }       
        public DeliveryStatus DeliveryStatus { get; set; }        
        public required int TotalQuantity { get; set; }
        public required decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }
        public required Guid CourierId { get; set; }
        public required DateTime EstimatedDeliveryTime { get; set; }        
    }
}
