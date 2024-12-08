using System.ComponentModel.DataAnnotations;

namespace DeliveryService.DTO
{
    public class UpdateDeliveryDTO
    {
        public required Guid Id { get; set; }
        public Guid Order_Id { get; set; }
        //public PaymentType PaymentType { get; set; }  //UpdatePayment from OrderService??
        public required decimal Total_Price { get; set; }
        public required string Shipping_Address { get; set; }
        public required DateTime Delivery_Time { get; set; }
    }
}