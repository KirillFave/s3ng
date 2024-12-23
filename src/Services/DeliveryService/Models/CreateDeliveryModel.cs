using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Domain.External.Entities;
using DeliveryService.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models
{
    public class CreateDeliveryModel
    {
        public required Guid UserGuid { get; set; }
        public Guid Order_Id { get; set; }
        [Required]
        [EnumDataType(typeof(DeliveryStatus))]
        [JsonConverter(typeof(StringEnumConverter))]
        public DeliveryStatus DeliveryStatus { get; set; }
        public required int Total_Quantity { get; set; }
        public required decimal Total_Price { get; set; }
        public PaymentType PaymentType { get; set; }        
        public required string Shipping_Address { get; set; }
        public Guid Courier_Id { get; set; }        
        public required DateTime Estimated_Delivery_Time { get; set; }                  
    }
}