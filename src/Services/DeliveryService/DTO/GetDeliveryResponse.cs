using System.ComponentModel.DataAnnotations;

namespace DeliveryService.DTO
{
    public class GetDeliveryResponse
    {
        public required Guid Id { get; set; }
        public Guid Order_Id { get; set; }
        //public required OrderStatus OrderStatus { get; set; }
        public required int Total_Quantity { get; set; }
        public required decimal Total_Price { get; set; }
        public required string Shipping_Address { get; set; }
        public required DateTime Delivery_Time { get; set; }
    }
}


