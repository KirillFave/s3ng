using DeliveryService.Delivery.BusinessLogic.Enums;
using DeliveryService.Delivery.DataAccess.Domain.External.Entities;
using System.Diagnostics.Metrics;

namespace DeliveryService.Delivery.DataAccess.Domain.Domain.Entities
{
    public class Delivery : IEntity<Guid>
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Delivery basic parameters
        /// </summary>         
        public DeliveryStatus DeliveryStatus { get; set; }
        public Guid OrderId { get; set; }
        public Guid CourierId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }
        public required Courier Courier { get; set; }
        public required Order Order { get; set; }
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
        public DateTime TimeModified { get; set; }
    }
}
