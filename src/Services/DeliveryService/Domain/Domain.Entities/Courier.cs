using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Domain.Domain.Entities
{
    public class Courier : IEntity<Guid>
    
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }    
        public virtual List<Delivery> ? Deliveries { get; set; }
    }
}
