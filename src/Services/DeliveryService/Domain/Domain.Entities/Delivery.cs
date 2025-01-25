using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using DeliveryService.Domain.External.Entities;
using DeliveryService.Enums;

namespace DeliveryService.Domain.Domain.Entities
{
    public class Delivery : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Delivery basic parameters
        /// </summary>         
        public DeliveryStatus DeliveryStatus { get; set; }
        public Guid OrderId { get; set; }
        public Guid ? UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public string ? ShippingAddress { get; set; }
        public virtual Courier ? Courier { get; set; }          
        /// <summary>
        /// Delivery timing
        /// </summary>
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime ActualDeliveryTime { get; set; }
        public DateTime CreateTimestamp { get; set; }
        /// <summary>
        /// Delivery modify status
        /// </summary>           
        public bool IsDeleted { get; set; }
        public DateTime TimeModified { get; set; }
    }    
}
