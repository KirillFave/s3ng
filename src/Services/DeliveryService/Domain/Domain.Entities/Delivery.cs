using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Domain.Domain.Entities
{
    public class Delivery : IEntity<Guid>
    {
        [Key]
        public required Guid Id { get; set; }
        public required string OrderStatus { get; set; }
        public required decimal Total_Price { get; set; }
        public required decimal Total_Quantity { get; set; }
        
        [MaxLength(200)]
        public required string Shipping_Address { get; set; }
        //public PaymentType PaymentType { get; set; }
        public int Order_Id { get; set; }
        public int Courer_Id { get; set; }
        public DateTime Delivery_Time { get; set; }
        public DateTime CreateTimeestamp { get; set; }

        public Delivery() : base() { }
    }
}
