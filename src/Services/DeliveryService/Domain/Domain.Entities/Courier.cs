using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Domain.Domain.Entities
{
    public class Courier 
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }        
              
        public virtual Delivery ? Delivery { get; set; }
    }
}
