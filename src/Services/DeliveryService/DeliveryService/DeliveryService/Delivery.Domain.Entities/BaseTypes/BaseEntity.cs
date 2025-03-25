using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DeliveryService.Delivery.Domain.Entities.BaseTypes
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
