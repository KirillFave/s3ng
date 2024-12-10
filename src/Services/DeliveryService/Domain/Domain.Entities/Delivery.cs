using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using DeliveryService.Domain.External.Entities;

namespace DeliveryService.Domain.Domain.Entities
{
    public class Delivery : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
                
        public Guid Order_Id { get; set; }       
        
        public required Guid UserGuid {  get; set; }

        public OrderStatus OrderStatus { get; set; }

        public required int Total_Quantity { get; set; }
        public decimal Total_Price { get; set; }        
        
        public PaymentType PaymentType { get; set; }
        
        [MaxLength(200, ErrorMessage = "Length must be less then 200 characters")]
        public required string Shipping_Address { get; set; }        
        
        public Guid CourierId { get; set; }
        
        public required DateTime Delivery_Time { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        public  List<OrderItem> ? Items { get; set; }
        public virtual List<Courier> ? Couriers { get; set; }
    }
}
