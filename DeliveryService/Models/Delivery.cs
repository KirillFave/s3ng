using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Models
{
    public class Delivery
    {
        [Key]
        public Guid Id { get; set; }
        public string OrderStatus { get; set; }
        public decimal Total_Price { get; set; }
        [MaxLength(200)]
        public string Shipping_Address { get; set; } 
        //public PaymentType PaymentType { get; set; }
        public int Order_Id {  get; set; }
        public int Courer_Id { get; set; }
        public DateTime Delivery_Time { get; set; }
    }
}
