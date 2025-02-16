using AutoMapper;
using DeliveryService.Delivery.BusinessLogic.Models;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Delivery.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Delivery.Core.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IMapper _mapper;
        private readonly ILogger<DeliveryController> _logger;

        public DeliveryController(IDeliveryService deliveryService, IMapper mapper, ILogger<DeliveryController> logger)
        {
            _deliveryService = deliveryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("/api/delivery/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var deliveryModel = _mapper.Map<DeliveryModel>(await _deliveryService.GetByIdAsync(id));
            return Ok(_mapper.Map<DeliveryDBContext>(await _deliveryService.GetByIdAsync(id)));
        }

        [HttpPost("/api/create-delivery")]
        public async Task<IActionResult> CreateAsync(CreateDeliveryModel createDeliveryModel)
        {
            //var model = createDeliveryModel.MapModelDelivery();
            return Ok(await _deliveryService.CreateAsync(_mapper.Map<CreateDeliveryDto>(createDeliveryModel)));
            //return Ok(await _deliveryService.CreateAsync(model));
        }

        [HttpPut("/api/update-delivery/{id}")]
        public async Task<IActionResult> TryUpdateAsync(Guid id, UpdateDeliveryModel updateDeliveryModel)
        {
            //var updateDeliveryDTO = _mapper.Map<UpdateDeliveryDto>(updateDeliveryDto);

            await _deliveryService.TryUpdateAsync(id, _mapper.Map<UpdateDeliveryModel, UpdateDeliveryDto>(updateDeliveryModel));
            //bool isUpdated = await _deliveryService.TryUpdateAsync(id, updateDeliveryModel);
            //return isUpdated ? Ok() : NotFound($"Доставка с идентфикатором {id} не найдена");
            return Ok();
        }

        [HttpDelete("/api/delete-delivery/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            bool isDeleted = await _deliveryService.TryDeleteAsync(id);
            return isDeleted ? Ok(id) : NotFound();
        }
    }
}