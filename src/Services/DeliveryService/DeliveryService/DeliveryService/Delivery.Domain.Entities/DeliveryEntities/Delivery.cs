using DeliveryService.Delivery.BusinessLogic.Enums;
using DeliveryService.Domain.External.Entities;
using System.Diagnostics.Metrics;

namespace DeliveryService.Delivery.Domain.Entities.DeliveryEntities
{
    public class Delivery : IEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        /// <summary>
        /// Delivery basic parameters
        /// </summary>         
        public Guid UserGuid { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; } = DeliveryStatus.AwaitingShipment;
        public Guid OrderId { get; set; }
        public OrderState OrderStatus { get; set; }
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
