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
        public Guid OrderId { get; set; }               
        public required Guid UserId {  get; set; }
        public OrderStatus OrderStatus { get; set; }
        public required int TotalQuantity { get; set; }
        public required decimal TotalPrice { get; set; }        
        
        public PaymentType PaymentType { get; set; }              
       
        public required string ShippingAddress { get; set; }        
        public Guid ? CourierId { get; set; }
        /// <summary>
        /// Delivery timing
        /// </summary>
        public required DateTime EstimatedDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }
        public DateTime CreateTimestamp { get; set; }    
        /// <summary>
        /// Details of Delivery
        /// </summary>           
        public bool IsDeleted { get; set; }        
        public DateTime TimeModified { get; set; }
    }
}
