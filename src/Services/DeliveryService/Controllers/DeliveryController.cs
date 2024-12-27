using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Data;
using DeliveryService.DTO;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Abstractions;
using DeliveryService.Repositories;
using System.Threading;
using DeliveryService.Models;
using AutoMapper;

namespace DeliveryService.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class DeliveryController : Controller
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeliveryController> _logger;

        public DeliveryController(IDeliveryRepository deliveryRepository, IMapper mapper, ILogger<DeliveryController> logger)
        {
            _deliveryRepository = deliveryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("GetDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<GetDeliveryDTO>), 200)]
        public async Task<ActionResult> GetByIdAsync(Guid guid, CancellationToken cancellationToken)
        {
            Delivery delivery = await _deliveryRepository.GetByIdAsync(guid, cancellationToken);

            return delivery is null ? NotFound() : Ok(delivery);
        }

        [HttpPost ("CreateDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<CreateDeliveryDTO>), 200)]
        public async Task<ActionResult> AddAsync(CreateDeliveryModel createDeliveryModel)
        {
            var createDeliveryDTO = _mapper.Map<CreateDeliveryDTO>(createDeliveryModel);
            var createDeliveryGuid = await _deliveryRepository.AddAsync(createDeliveryDTO);

            return Created("", createDeliveryModel);
        }

        [HttpPut("UpdateDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<UpdateDeliveryDTO>), 200)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateDeliveryModel updateDeliveryModel)
        {
            var updateDeliveryDTO = _mapper.Map<UpdateDeliveryDTO>(updateDeliveryModel);
            bool isUpdated = await _deliveryRepository.TryUpdateAsync(id, updateDeliveryDTO);

            return isUpdated ? Ok() : NotFound($"Delivery with {id} not found");            
        }

        [HttpDelete("DeleteDelivery")]
        public async Task<ActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            OperationResults result = await _deliveryRepository.DeleteAsync(id, cancellationToken);

            return result switch
            {
                OperationResults.NoEntityFound => NotFound(),
                OperationResults.Success => NoContent(),
                OperationResults.NoChangesApplied => StatusCode(500, $"Failed to delete the {nameof(Delivery)} to the database."),
                _ => throw new NotImplementedException(),
            };
        }
    }
}

