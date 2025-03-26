using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Delivery.Domain.Entities.DeliveryEntities
{
    public class Route
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
