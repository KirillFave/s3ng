using DeliveryService.Delivery.BusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;
using DeliveryService.Delivery.DataAccess.Domain.Domain.Entities;
using DeliveryService.Delivery.BusinessLogic.Models;

namespace DeliveryService.Delivery.DataAccess.Domain.External.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public List<OrderItem>? Items { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }
        public DateTime CreatedTimestamp { get; set; }

        public DeliveryService.Delivery.DataAccess.Domain.Domain.Entities.Delivery? Delivery { get; set; } 
    }
}
