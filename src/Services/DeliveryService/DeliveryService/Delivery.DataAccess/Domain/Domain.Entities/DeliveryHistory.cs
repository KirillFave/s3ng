using DeliveryService.Delivery.BusinessLogic.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace DeliveryService.Delivery.DataAccess.Domain.Domain.Entities
{
    public class DeliveryHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DeliveryStatus DeliveryStatus { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Comments { get; set; }
        public Guid DeliveryId { get; set; }
        [JsonIgnore] public Delivery? Delivery { get; set; }
    }
}
