using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Delivery.DataAccess.Domain.Domain.Entities
{
    public class Courier

    {        
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public virtual List<Delivery>? Deliveries { get; set; }
    }
}
