using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Domain.External.Entities;
using DeliveryService.Enums;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models
{
    public class DeliveryMessage
    {
        public Guid Delivery_Id { get; set; }       
        public DeliveryStatus DeliveryStatus { get; set; }
        public Guid Order_Id { get; set; }
        public Guid UserGuid { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int Total_Quantity { get; set; }
        public decimal Total_Price { get; set; }
        public PaymentType PaymentType { get; set; }        
        public string Shipping_Address { get; set; }
        public Guid CourierId { get; set; }        
        public required DateTime Estimated_Delivery_Time { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public List<OrderItem>? Items { get; set; }       
    }
}
