using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Domain.Domain.Entities
{
    public class Courer : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public string ? Name { get; set; }

        [Required]
        public Guid Delivery_Id { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Length must be less then 200 characters")]
        public required string Shipping_Address { get; set; }
    }
}
