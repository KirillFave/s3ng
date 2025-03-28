using SharedLibrary.DeliveryService.Models;
using SharedLibrary.OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DeliveryService.Dto
{
    class UpdateDeliveryDto
    {
        public Guid Id { get; set; }
        public required Guid UserGuid { get; set; }
        public Guid OrderId { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        public required int TotalQuantity { get; set; }
        public required decimal TotalPrice { get; set; }
        public required string ShippingAddress { get; set; }
        public required DateTime EstimatedDeliveryTime { get; set; }
    }
}
