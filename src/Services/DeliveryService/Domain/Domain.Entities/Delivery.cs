using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Domain.Domain.Entities
{
    public class Delivery : IEntity<Guid>
    {
        [Key]
        public required Guid Id { get; set; }
        public required Guid Order_Id { get; set; }
        //public List<OrderItem> Items { get; set; }
        public required Guid UserGuid {  get; set; } 
        
        //public required OrderStatus OrderStatus { get; set; }
        public required int Total_Quantity { get; set; }
        public required decimal Total_Price { get; set; }
        //public PaymentType PaymentType { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Length must be less then 200 characters")]
        public required string Shipping_Address { get; set; }        
        
        public required Guid Courer_Id { get; set; }
        public required DateTime Delivery_Time { get; set; }
        public DateTime CreateTimeestamp { get; set; }
        public bool IsDeleted { get; set; }               
    }
}
