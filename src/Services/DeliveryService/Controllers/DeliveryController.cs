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

        [HttpGet("GetDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<GetDeliveryDTO>), 200)]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok(_mapper.Map<DeliveryDBContext>(await _deliveryService.GetByIdAsync(id)));
        }

        [HttpPost("CreateDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<CreateDeliveryDTO>), 200)]
        public async Task<IActionResult> CreateAsync(CreateDeliveryModel createDeliveryModel)
        {
            var createDeliveryDTO = _mapper.Map<CreateDeliveryDTO>(createDeliveryModel);
            var createDeliveryGuid = await _deliveryService.CreateAsync(createDeliveryDTO);

            return Created("", createDeliveryGuid);
        }

        [HttpPut("UpdateDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<UpdateDeliveryDTO>), 200)]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateDeliveryModel updateDeliveryModel)
        {
            var updateDeliveryDTO = _mapper.Map<UpdateDeliveryDTO>(updateDeliveryModel);
            bool isUpdated = await _deliveryService.TryUpdateAsync(id, updateDeliveryDTO);

            return isUpdated ? Ok() : NotFound($"Доставка с идентфикатором {id} не найдена");
        }

        [HttpDelete("DeleteDelivery")]
        public async Task<IActionResult> DeleteAsync(Guid id) 
        {
            bool isDeleted = await _deliveryService.TryDeleteAsync(id);

            return isDeleted ? Ok(id) : NotFound();
        }        
    }
}

