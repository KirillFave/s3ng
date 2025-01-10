using AutoMapper;
using DeliveryService.Controllers;
using DeliveryService.Services.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeliveryService.Application.Test
{
    public class DeliveryControllerTest
    {
        private readonly DeliveryController _controller;
        private readonly IDeliveryService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<DeliveryController> _logger;
        public DeliveryControllerTest()
        {
            _controller = new DeliveryController(_service, _mapper, _logger);
        }
        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            //Act
            var notFoundResult = _controller.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }
        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            // Act
            var okResult = _controller.GetByIdAsync(testGuid);
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            // Act
            var okResult = _controller.GetByIdAsync(testGuid);// as OkObjectResult;
            // Assert
            Assert.IsType<Guid>(okResult.Status);
            //Guid guid = (okResult.Id as Guid);
            //Assert.Equal(testGuid, guid.Id);
        }
    }
}