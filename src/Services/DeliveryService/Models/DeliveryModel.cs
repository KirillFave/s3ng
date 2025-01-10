// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DeliveryService.Enums;

namespace DeliveryService.Models
{  
    public class DeliveryModel
        {
            public required Guid UserId { get; set; }
            public Guid OrderId { get; set; }
            public OrderStatus orderStatus { get; set; }
            public DeliveryStatus DeliveryStatus { get; set; }
            public required int TotalQuantity { get; set; }
            public required decimal TotalPrice { get; set; }
            public PaymentType PaymentType { get; set; }
            public required string ShippingAddress { get; set; }
            public Guid CourierId { get; set; }
            public required DateTime EstimatedDeliveryTime { get; set; }
            public DateTime ActualDeliveryTime { get; set; }
    }
}

