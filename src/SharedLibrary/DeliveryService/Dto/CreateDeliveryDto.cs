using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLibrary.OrderService.Models;

namespace SharedLibrary.DeliveryService.Dto
{
    class CreateDeliveryDto
    {
        public Guid Id { get; set; }
        public required Guid UserGuid { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public PaymentType PaymentType { get; set; }
        public required string ShippingAddress { get; set; }
        public DateTime EstimatedDeliveryTime { get; set; }
    }
}
