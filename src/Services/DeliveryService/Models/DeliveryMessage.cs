using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Domain.External.Entities;
using DeliveryService.Enums;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models
{
    public class DeliveryMessage
    {
        public Guid DeliveryId { get; set; }       
        public DeliveryStatus DeliveryStatus { get; set; }
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }        
        public string ShippingAddress { get; set; }
        public Guid CourierId { get; set; }        
        public required DateTime EstimatedDeliveryTime { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public List<OrderItem>? Items { get; set; }       
    }
}
