using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Data;
using DeliveryService.DTO;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Repositories;
using System.Threading;

namespace DeliveryService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DeliveryController : Controller
    {
        private readonly DeliveryRepository _deliveryRepository;

        public DeliveryController(DeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetDeliveryDTO>), 200)]
        public async Task<ActionResult> Get(Guid guid, CancellationToken cancellationToken)
        {
            Delivery delivery = await _deliveryRepository.GetByIdAsync(guid, cancellationToken);

            return delivery is null ? NotFound() : Ok(delivery);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<CreateDeliveryDTO>), 200)]
        public async Task<ActionResult> Create(Delivery delivery, CancellationToken cancellationToken)
        {
            bool result = await _deliveryRepository.AddAsync(delivery, cancellationToken);

            return result ? NoContent() : Created();
        }

        [HttpPatch]
        [ProducesResponseType(typeof(IEnumerable<UpdateDeliveryDTO>), 200)]
        public async Task<ActionResult> Update(Delivery delivery, CancellationToken cancellationToken)
        {
            OperationResults result = await _deliveryRepository.UpdateAsync(delivery,);

            return result switch
            {
                OperationResults.NoEntityFound => NotFound(),
                OperationResults.Success => NoContent(),
                OperationResults.NoChangesApplied => StatusCode(500, $"Failed to save the {nameof(Delivery)} to the database."),
                _ => throw new NotImplementedException(),
            };
        }

        [HttpDelete]
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

