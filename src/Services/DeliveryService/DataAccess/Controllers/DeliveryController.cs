using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DeliveryService.DataAccess.Domain.Domain.Entities;
using DeliveryService.Extentions;
using DeliveryService.BL.Services.Services.Abstractions;
using DeliveryService.BL.Services.Services.Contracts.DTO;
using DeliveryService.DataAccess.Data;
using DeliveryService.DataAccess.Models;

namespace DeliveryService.Controllers
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
            return Ok(await _deliveryService.CreateAsync(_mapper.Map<CreateDeliveryDTO>(createDeliveryModel)));
            //return Ok(await _deliveryService.CreateAsync(model));
        }     

        [HttpPut("/api/update-delivery/{id}")]
        public async Task<IActionResult> TryUpdateAsync(Guid id, UpdateDeliveryModel updateDeliveryModel)
        {
            //var updateDeliveryDTO = _mapper.Map<UpdateDeliveryDTO>(updateDeliveryDTO);
  
            await _deliveryService.TryUpdateAsync(id, _mapper.Map<UpdateDeliveryModel, UpdateDeliveryDTO>(updateDeliveryModel));
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

