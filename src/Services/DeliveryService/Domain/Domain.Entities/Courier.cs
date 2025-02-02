using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Domain.Domain.Entities
{
    //[Table("Courier")]
    public class Courier 
    
    {
        //[Column("CourierId")]
        [Key]
        public Guid Id { get; set; }        
        public required string Name { get; set; }    
        public virtual List<Delivery> ? Deliveries { get; set; }
    }
}
