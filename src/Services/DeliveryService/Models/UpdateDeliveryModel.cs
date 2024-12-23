using DeliveryService.Enums;

namespace DeliveryService.Models
{
    public class UpdateDeliveryModel
    {       
        public Guid Order_Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public int Total_Quantity { get; set; }
        public decimal Total_Price { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string Shipping_Address { get; set; }
        public Guid Courier_Id { get; set; }
        public DateTime Estimated_Delivery_Time { get; set; }
    }
}
