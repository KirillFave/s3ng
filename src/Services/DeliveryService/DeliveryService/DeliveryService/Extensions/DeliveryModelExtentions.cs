﻿using DeliveryService.Delivery.BusinessLogic.Models;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;

namespace DeliveryService.Extensions
{
    public static class DeliveryModelExtentions
    {
        public static CreateDeliveryDto MapModelDelivery(this CreateDeliveryModel dtoEntity)

        {
            return new CreateDeliveryDto
            {
                Id = dtoEntity.Id,
                UserGuid = dtoEntity.UserGuid,
                OrderId = dtoEntity.OrderId,
                OrderStatus = dtoEntity.OrderStatus,                
                DeliveryStatus = dtoEntity.DeliveryStatus,
                TotalQuantity = dtoEntity.TotalQuantity,
                TotalPrice = dtoEntity.TotalPrice,
                PaymentType = dtoEntity.PaymentType,
                ShippingAddress = dtoEntity.ShippingAddress,
                EstimatedDeliveryTime = dtoEntity.EstimatedDeliveryTime
            };

        }
    }
}
