using System;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Enums;
using DeliveryService.Models;
using DeliveryService.Services.Services.Contracts.DTO;

    // Licensed to the .NET Foundation under one or more agreements.
    // The .NET Foundation licenses this file to you under the MIT license. 

namespace DeliveryService.Extentions
{
    public static class DeliveryExtentions
    {      
            public static CreateDeliveryDTO MapDelivery(this Delivery dtoDEntity)

            {
                return new CreateDeliveryDTO
                {
                    Id = dtoDEntity.Id,
                    DeliveryStatus = dtoDEntity.DeliveryStatus,
                    UserId = dtoDEntity.UserId,
                    OrderId = dtoDEntity.OrderId,
                    OrderStatus = dtoDEntity.OrderStatus,                   
                    TotalQuantity = dtoDEntity.TotalQuantity,
                    TotalPrice = dtoDEntity.TotalPrice,
                    PaymentType = dtoDEntity.PaymentType,
                    ShippingAddress = dtoDEntity.ShippingAddress,                 //? is null here   
                    EstimatedDeliveryTime = dtoDEntity.EstimatedDeliveryTime
                };
            }
    }
}
