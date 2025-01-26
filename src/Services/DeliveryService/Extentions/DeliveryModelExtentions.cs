using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Services.Services.Contracts.DTO;
using DeliveryService.Models;
using DeliveryService.Enums;


// Licensed to the .NET Foundation under one or more agreements.DeliveryService.Domain.Domain.Entities
// The .NET Foundation licenses this file to you under the MIT license.

namespace DeliveryService.Extentions
{
    public static class DeliveryModelExtentions
    {
        public static CreateDeliveryDTO MapModelDelivery(this CreateDeliveryModel dtoEntity)

        {
            return new CreateDeliveryDTO 
            {
                Id = dtoEntity.Id,
                UserId = dtoEntity.UserId,
                OrderId = dtoEntity.OrderId,
                OrderStatus = dtoEntity.OrderStatus,
                DeliveryStatus = dtoEntity.DeliveryStatus,
                TotalQuantity = dtoEntity.TotalQuantity,
                TotalPrice = dtoEntity.TotalPrice,
                PaymentType = dtoEntity.PaymentType,
                ShippingAddress = dtoEntity.ShippingAddress,
                CourierId = dtoEntity.CourierId,
                EstimatedDeliveryTime = dtoEntity.EstimatedDeliveryTime    
            };
        }
    }
}
