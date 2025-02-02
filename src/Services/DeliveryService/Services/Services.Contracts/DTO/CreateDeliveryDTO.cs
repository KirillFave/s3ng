using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Domain.External.Entities;
using DeliveryService.Enums;

namespace DeliveryService.Services.Services.Contracts.DTO
{
    public class CreateDeliveryDTO
    {
        public Guid Id { get; set; }
       // public Guid ? UserId { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }       
        public DeliveryStatus DeliveryStatus { get; set; }        
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }
        public required Courier Courier { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }        
    }
}
