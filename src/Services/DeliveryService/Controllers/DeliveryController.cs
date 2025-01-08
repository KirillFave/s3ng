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

        [HttpGet("/api/product/{id}")]        
        public async Task<IActionResult> GetAsync(Guid id)
        {            
            var deliveryModel = _mapper.Map<DeliveryModel>(await _deliveryService.GetByIdAsync(id));
            return Ok(_mapper.Map<DeliveryDBContext>(await _deliveryService.GetByIdAsync(id)));
        }

        [HttpPost("/api/create-product")]        
        public async Task<IActionResult> CreateAsync(CreateDeliveryModel deliveryModel)
        {
            var createDeliveryDTO = _mapper.Map<CreateDeliveryDTO>(deliveryModel);
            return Ok(await _deliveryService.CreateAsync(_mapper.Map<CreateDeliveryDTO>(deliveryModel)));
        }

        //[HttpPut("UpdateDelivery")]        
        //public async Task<IActionResult> UpdateAsync(Guid id, UpdateDeliveryDTO updateDeliveryDTO)
        //{
        //    var updateDeliveryDTO = _mapper.Map<UpdateDeliveryDTO>(updateDeliveryDTO);
        //    bool isUpdated = await _deliveryService.TryUpdateAsync(id, updateDeliveryModel);

        //    return isUpdated ? Ok() : NotFound($"Доставка с идентфикатором {id} не найдена");
        //}

        [HttpDelete("DeleteDelivery")]
        public async Task<IActionResult> DeleteAsync(Guid id) 
        {
            bool isDeleted = await _deliveryService.TryDeleteAsync(id);

            return isDeleted ? Ok(id) : NotFound();
        }        
    }
}

