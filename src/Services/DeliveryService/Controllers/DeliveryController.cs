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
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok(_mapper.Map<DeliveryDBContext>(await _deliveryRepository.GetAsync(id, cancellationToken)));
        }

        [HttpPost ("CreateDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<CreateDeliveryDTO>), 200)]
        public async Task<IActionResult> CreateAsync(CreateDeliveryModel createDeliveryModel)
        {
            var createDeliveryDTO = _mapper.Map<CreateDeliveryDTO>(createDeliveryModel);
            //var createDeliveryGuid = await _deliveryRepository.AddAsync(createDeliveryDTO);   
                        
            //return Created("", createDeliveryGuid);
            return Ok();
        }

        //[HttpPut("UpdateDelivery")]
        ////[ProducesResponseType(typeof(IEnumerable<UpdateDeliveryDTO>), 200)]
        //public async Task<IActionResult> UpdateAsync(Delivery delivery, UpdateDeliveryModel updateDeliveryModel)
        //{
        //    var updateDeliveryDTO = _mapper.Map<UpdateDeliveryDTO>(updateDeliveryModel);
        //     bool isUpdated = await _deliveryRepository.UpdateAsync(delivery, updateDeliveryDTO);
            
        //    return isUpdated ? Ok() : NotFound($"Доставка с идентфикатором {delivery} не найдена");            
        //}

        //[HttpDelete("DeleteDelivery")]
        //public async Task<IActionResult> Delete(Delivery delivery)
        //{
        //    bool isDeleted = await _deliveryRepository.DeleteAsync(delivery);

        //    return isDeleted ? Ok() : NotFound();
        //}
    }
}

