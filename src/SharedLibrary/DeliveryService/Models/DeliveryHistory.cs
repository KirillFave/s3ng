using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedLibrary.DeliveryService.Models
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
