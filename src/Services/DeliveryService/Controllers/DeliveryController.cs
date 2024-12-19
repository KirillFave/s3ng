using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Data;
using DeliveryService.DTO;
using DeliveryService.Domain.Domain.Entities;
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

        public DeliveryController(IDeliveryRepository deliveryRepository, IMapper mapper)
        {
            _deliveryRepository = deliveryRepository;
            _mapper = mapper;
        }

        [HttpGet("GetDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<GetDeliveryDTO>), 200)]
        public async Task<ActionResult> Get(Guid guid, CancellationToken cancellationToken)
        {
            Delivery delivery = await _deliveryRepository.GetByIdAsync(guid, cancellationToken);

            return delivery is null ? NotFound() : Ok(delivery);
        }

        [HttpPost ("CreateDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<CreateDeliveryDTO>), 200)]
        public async Task<ActionResult> CreateAsync(CreateDeliveryModel createDeliveryModel)
        {
            var createDeliveryDTO = _mapper.Map<CreateDeliveryDTO>(createDeliveryModel);
            var createDeliveryGuid = await _deliveryRepository.CreateAsync(createDeliveryDTO);

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
        public async Task<ActionResult> Delete(Guid guid)
        {
            OperationResults result = await _deliveryRepository.DeleteAsync(guid);

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

