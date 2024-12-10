using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Domain.Domain.Entities
{
    public class Courier : IEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public int Delivery_Id { get; set; }      

        
        [MaxLength(200, ErrorMessage = "Length must be less then 200 characters")]
        public required string Shipping_Address { get; set; }

        [Required]
        public virtual Delivery Delivery { get; set; }
    }
}
