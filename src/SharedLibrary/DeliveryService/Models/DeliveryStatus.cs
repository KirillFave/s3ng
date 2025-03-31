using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DeliveryService.Models
{
    public enum DeliveryStatus
    {
        AwaitingShipment = 1,
        Shipped = 2,
        InTransit = 3,
        Delivered = 4,
        Cancelled = 5
    }
}
