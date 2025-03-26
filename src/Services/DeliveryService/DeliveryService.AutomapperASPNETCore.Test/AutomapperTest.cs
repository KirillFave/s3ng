using AutoMapper;
using Microsoft.VisualStudio.TestPlatform.Common.Exceptions;
using DeliveryService.Delivery.Core.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata;
using DeliveryService.Delivery.BusinessLogic.Models;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Delivery.BusinessLogic;
using NHibernate.Cfg.ConfigurationSchema;
using DeliveryService.Delivery.Core.Mapping;
using Microsoft.Extensions.Caching.Distributed;
using Castle.Components.DictionaryAdapter.Xml;
using NHibernate.Linq;
using DeliveryService.Delivery.BusinessLogic.Enums;
using DeliveryService.Delivery.Core.Models.Requests;

namespace DeliveryService.AutomapperASPNETCore.Test
{
    public class AutomapperTest
    {
        private readonly IMapper? _mapper;

        public AutomapperTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DeliveryEditMappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public void EditDeliveryRequestToUpdateDeliveryDtoMappingTest()
        {
            //Assert
            var editDeliveryRequest = new EditDeliveryRequest
            {                
                UserGuid = Guid.Parse("f28e72eA-5bd0-4f14-8a78-9700ecdaf436"),
                OrderId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                DeliveryStatus = DeliveryStatus.AwaitingShipment,
                PaymentType = PaymentType.Card,
                TotalQuantity = 20,
                TotalPrice = 200,
                ShippingAddress = "HELL",
                EstimatedDeliveryTime = DateTime.UtcNow.AddDays(1)
            };

            //Act
            var updateDeliveryDto = _mapper.Map<UpdateDeliveryDto>(editDeliveryRequest);

            //Assert
            Assert.Equal(editDeliveryRequest.Id, updateDeliveryDto.Id);
            Assert.Equal(editDeliveryRequest.OrderId, updateDeliveryDto.OrderId);
            Assert.Equal(editDeliveryRequest.DeliveryStatus, updateDeliveryDto.DeliveryStatus);
            Assert.Equal(editDeliveryRequest.PaymentType, updateDeliveryDto.PaymentType);
            Assert.Equal(editDeliveryRequest.TotalQuantity, updateDeliveryDto.TotalQuantity);
            Assert.Equal(editDeliveryRequest.TotalPrice, updateDeliveryDto.TotalPrice);
            Assert.Equal(editDeliveryRequest.TotalQuantity, updateDeliveryDto.TotalQuantity);
            Assert.Equal(editDeliveryRequest.ShippingAddress, updateDeliveryDto.ShippingAddress);
            Assert.Equal(editDeliveryRequest.EstimatedDeliveryTime, updateDeliveryDto.EstimatedDeliveryTime);
        }
    }
}


