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
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            return Ok(_mapper.Map<DeliveryDBContext>(await _deliveryRepository.GetByIdAsync(id)));
        }

        [HttpPost ("CreateDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<CreateDeliveryDTO>), 200)]
        public async Task<ActionResult> AddAsyncCreateAsyn(CreateDeliveryDTO createDeliveryModel)
        {
            return Ok(await _deliveryRepository.CreateAsync(_mapper.Map<CreateDeliveryDTO>(createDeliveryModel)));
        }

        [HttpPut("UpdateDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<UpdateDeliveryDTO>), 200)]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdateDeliveryDTO updateDeliveryModel)
        {
            await _deliveryRepository.TryUpdateAsync(id, _mapper.Map<UpdateDeliveryDTO, UpdateDeliveryDTO>(updateDeliveryModel));
            return Ok();
        }

        [HttpDelete("DeleteDelivery")]
        public async Task<ActionResult> TryDeleteAsync(Guid id)
        {
            await _deliveryRepository.TryDeleteAsync(id);

            return Ok();            
        }
    }
}

