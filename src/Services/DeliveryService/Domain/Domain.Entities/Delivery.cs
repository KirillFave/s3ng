using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using DeliveryService.Domain.External.Entities;
using DeliveryService.Enums;

namespace DeliveryService.Domain.Domain.Entities
{
    public class Delivery 
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Delivery basic parameters
        /// </summary>         
        public DeliveryStatus DeliveryStatus { get; set; }
        public Guid Order_Id { get; set; }               
        public required Guid UserGuid {  get; set; }
        public OrderStatus OrderStatus { get; set; }
        public required int Total_Quantity { get; set; }
        public required decimal Total_Price { get; set; }        
        
        public PaymentType PaymentType { get; set; }                
        [MaxLength(200, ErrorMessage = "Length must be less then 200 characters")]
        public required string Shipping_Address { get; set; }        
        public Guid CourierId { get; set; }
        /// <summary>
        /// Delivery timing
        /// </summary>
        public required DateTime Estimated_Delivery_Time { get; set; }
        public DateTime Actual_Delivery_Time { get; set; }
        public DateTime CreateTimestamp { get; set; }    
        /// <summary>
        /// Details of Delivery
        /// </summary>
        [Required]
        public  List<OrderItem> ? Items { get; set; }
        public virtual List<Courier> ? Couriers { get; set; }
        public bool IsDeleted { get; set; }

        public static implicit operator bool(Delivery v)
        {
            throw new NotImplementedException();
        }
    }
}
