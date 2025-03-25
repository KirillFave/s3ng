using DeliveryService.Delivery.BusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;
using DeliveryService.Delivery.BusinessLogic.Models;
using DeliveryService.Delivery.Domain.Entities.DeliveryEntities;

namespace DeliveryService.Domain.External.Entities
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public List<OrderItem>? Items { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderState OrderStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTimeOffset ChangedAt { get; set; } = DateTimeOffset.Now;

        public DeliveryService.Delivery.Domain.Entities.DeliveryEntities.Delivery? Delivery { get; set; }
    }
}
