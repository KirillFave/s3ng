using System.ComponentModel.DataAnnotations;
using DeliveryService.Enums;

namespace DeliveryService.DTO
{
    public class UpdateDeliveryDTO
    {
        public required Guid Id { get; set; }
        public Guid Order_Id { get; set; }
        public PaymentType PaymentType { get; set; }  
        public required decimal Total_Price { get; set; }
        public required string Shipping_Address { get; set; }
        public required DateTime Estimated_Delivery_Time { get; set; }
        public DateTime Actual_Delivery_Time { get; set; }
    }
}