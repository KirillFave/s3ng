using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DeliveryService.Models;
using DeliveryService.Data;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Services.Services.Abstractions;
using DeliveryService.Services.Services.Contracts.DTO;

namespace DeliveryService.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class DeliveryController : Controller
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

        [HttpPut("/api/create-delivery")]        
        public async Task<IActionResult> CreateAsync(CreateDeliveryModel createDeliveryModel)
        {            
            return Ok(await _deliveryService.CreateAsync(_mapper.Map<CreateDeliveryDTO>(createDeliveryModel)));
        }

        //[HttpPut("UpdateDelivery")]        
        //public async Task<IActionResult> UpdateAsync(Guid id, UpdateDeliveryDTO updateDeliveryDTO)
        //{
        //    var updateDeliveryDTO = _mapper.Map<UpdateDeliveryDTO>(updateDeliveryDTO);
        //    bool isUpdated = await _deliveryService.TryUpdateAsync(id, updateDeliveryModel);

        //    return isUpdated ? Ok() : NotFound($"Доставка с идентфикатором {id} не найдена");
        //}

        [HttpPatch("/api/update-delivery/{id}")]
        public async Task<IActionResult> TryUpdateAsync(Guid id, UpdateDeliveryModel updateDeliveryModel)
        {
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

