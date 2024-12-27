﻿using System.ComponentModel.DataAnnotations;
using DeliveryService.Domain.External.Entities;
using DeliveryService.Enums;

namespace DeliveryService.DTO
{
    public class CreateDeliveryDTO
    {
        public Guid Id { get; set; }
        public Guid Order_Id { get; set; }
        public required Guid UserGuid { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public List<OrderItem> ? Items { get; set; }        
        public required int Total_Quantity { get; set; }
        public required decimal Total_Price { get; set; }
        public required string Shipping_Address { get; set; }
        public required Guid Courer_Id { get; set; }
        public required DateTime Estimated_Delivery_Time { get; set; }
        public DateTime Actual_Delivery_Time { get; set; }
    }
}