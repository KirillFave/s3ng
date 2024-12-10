using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Domain.Domain.Entities
{
    public class Delivery : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid Order_Id { get; set; }
        //public List<OrderItem> Items { get; set; }
        
        [Required]
        public Guid UserGuid {  get; set; }

        //public required OrderStatus OrderStatus { get; set; }

        [Required]
        public int Total_Quantity { get; set; }
        
        [Required]
        public decimal Total_Price { get; set; }
        //public PaymentType PaymentType { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Length must be less then 200 characters")]
        public required string Shipping_Address { get; set; }
        
        [Required]
        public Guid Courer_Id { get; set; }

        [Required]
        public required DateTime Delivery_Time { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public bool IsDeleted { get; set; }               
    }
}
