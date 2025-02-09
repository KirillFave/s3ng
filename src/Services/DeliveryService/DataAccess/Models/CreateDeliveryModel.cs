using DeliveryService.BL.Enums;
using DeliveryService.DataAccess.Domain.Domain.Entities;
using DeliveryService.Domain.External.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.DataAccess.Models
{
    public class CreateDeliveryModel
    {
        public Guid Id { get; set; }
        //public required Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }
        public required Guid CourierId { get; set; }
        public required Courier Courier { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }
    }
}
