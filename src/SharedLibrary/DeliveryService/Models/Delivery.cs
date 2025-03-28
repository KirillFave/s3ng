using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.OrderService.Models;

namespace SharedLibrary.DeliveryService.Models
{
    public class Delivery 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Delivery basic parameters
        /// </summary>         
        public Guid UserGuid { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; } = DeliveryStatus.AwaitingShipment;
        public Guid OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; } = null!;
        public Order? Order { get; set; }
        /// <summary>
        /// Delivery timing
        /// </summary>
        public DateTime EstimatedDeliveryTime { get; set; }
        public DateTime? ActualDeliveryTime { get; set; }
        public DateTime? CreateTimestamp { get; set; }
        /// <summary>
        /// Delivery modify status
        /// </summary>           
        public bool IsDeleted { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual List<DeliveryHistory>? History { get; set; }
    }
}
