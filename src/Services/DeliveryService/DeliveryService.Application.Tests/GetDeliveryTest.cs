using AutoMapper;
using DeliveryService.Delivery.Core.Controllers;
using Microsoft.AspNetCore.Identity;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;
using Moq;
using Xunit;
using Microsoft.Win32;
using DeliveryService.Delivery.BusinessLogic.Enums;
using DeliveryService.Application.Tests.Helps;
using DeliveryService.Delivery.Domain.Entities.DeliveryEntities;
using AutoFixture.Xunit2;
using DeliveryService.Delivery.BusinessLogic.Services.DeliveryService;
using OpenQA.Selenium;

namespace DeliveryService.Application.Tests

{
    public class GetDeliveryTest
    {
        [Theory, AutoMoqData]
        public async Task GetByIdAsync_DeliveryExists_ReturnsDelivery(
        Guid id,
        DeliveryService.Delivery.Domain.Entities.DeliveryEntities.Delivery delivery,
        [Frozen] Mock<IDeliveryRepository> deliveryRepositoryMock,
        DeliveryService.Delivery.BusinessLogic.Services.DeliveryService.DeliveryServices deliveryService,
        CancellationToken ñancellationToken)
        {
            //// Arrange
            //delivery.Id = id;
            //deliveryRepositoryMock.Setup(repo => repo.GetByIdAsync(id, ñancellationToken /*, It.IsAny<string>()*/))
            //    .ReturnsAsync(delivery);

            //// Act
            //var result = await deliveryService.GetByIdAsync(id, ñancellationToken);

            //// Assert
            //result.Should().NotBeNull();
            //result.Id.Should().Be(id);
        }

        [Theory, AutoMoqData]
        public async Task GetByIdAsync_RecordNotFound_ThrowsNotFoundException(
        Guid id,
        [Frozen] Mock<IDeliveryRepository> deliveryRepositoryMock,
        DeliveryService.Delivery.BusinessLogic.Services.DeliveryService.DeliveryServices deliveryService,
        CancellationToken ñancellationToken)
        {
            // Arrange
            DeliveryService.Delivery.Domain.Entities.DeliveryEntities.Delivery delivery = null;
            deliveryRepositoryMock.Setup(repo => repo.GetAsync(id, ñancellationToken/*, It.IsAny<string>()*/))
                                    .ToString();

            // Act & Assert: ïðîâåðêà âûçûâà èñêëþ÷åíèÿ
            var exception = await Assert.ThrowsAsync<NotFoundException>(
                () => deliveryService.GetByIdAsync(id, ñancellationToken));

            Func<Task> act = async () => await deliveryService.GetByIdAsync(id, ñancellationToken);

            //Assert
            //await act.Should().ThrowAsync<NotFoundException>();
        }
    }
}
















//        private readonly DeliveryController _controller;
//        private readonly Mock<IDeliveryService> _mockService;
//        private readonly IMapper _mapper;
//        private readonly IDistributedCache _distributedCache;
//        public GetDeliveryTest()
//        {
//            _mockService = new Mock<IDeliveryService>();
//            _controller = new DeliveryController(_mockService.Object, _mapper, _distributedCache);
//        }

//        [Fact]
//        public void Test_Create_GET_ReturnsViewResultNullModel()
//        {
//            // Arrange
//            //IDeliveryService context = null;
//            var controller = new DeliveryController(_mockService.Object, _mapper, _distributedCache);

//            // Act
//            var result = controller.CreateAsync(new Guid("{4CD16C19-9A8A-4B2B-BA8C-2D3985EBD292}"), CancellationToken.None);

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.Null(viewResult.ViewData.Model);
//        }
//    }
//}