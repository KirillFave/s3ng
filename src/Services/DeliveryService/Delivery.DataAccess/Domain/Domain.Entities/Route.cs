﻿using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Delivery.DataAccess.Domain.Domain.Entities
{
    public class Route
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
