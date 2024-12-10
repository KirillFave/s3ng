using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Domain.Domain.Entities
{
    public class Route : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
