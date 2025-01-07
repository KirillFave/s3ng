using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Data;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Abstractions;
using System.Threading;
using DeliveryService.Models;
using AutoMapper;
using DeliveryService.Services.Services.Contracts.DTO;
using DeliveryService.Services.Services.Repositories;

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

        [HttpPost("CreateDelivery")]
        //[ProducesResponseType(typeof(IEnumerable<CreateDeliveryDTO>), 200)]
        public async Task<IActionResult> CreateAsync(CreateDeliveryModel createDeliveryModel)
        {
            var createDeliveryDTO = _mapper.Map<CreateDeliveryDTO>(createDeliveryModel);
           // var createDeliveryGuid = await _deliveryRepository.AddAsync(createDeliveryDTO);

            //return Created("", createDeliveryGuid);
            return Ok();
        }

        public IDeliveryRepository Get_deliveryRepository() => _deliveryRepository;

        //[HttpPut("UpdateDelivery")]
        ////[ProducesResponseType(typeof(IEnumerable<UpdateDeliveryDTO>), 200)]
        //public async Task<IActionResult> UpdateAsync(Delivery delivery, UpdateDeliveryModel updateDeliveryModel)
        //{
        //    var updateDeliveryDTO = _mapper.Map<UpdateDeliveryDTO>(updateDeliveryModel);
        //    bool isUpdated = await _deliveryRepository.UpdateAsync(delivery, );

        //    return isUpdated ? Ok() : NotFound($"Доставка с идентфикатором {delivery} не найдена");
        //}

        //[HttpDelete("DeleteDelivery")]
        //public async Task<IActionResult> DeleteAsync(Delivery delivery) //, CancellationToken cancellationToken) //IDeliveryRepository _deliveryRepository)
        //{
        //    bool isDeleted = await _deliveryRepository.DeleteAsync(delivery, HttpContext.RequestAborted);

        //    return isDeleted ? Ok(delivery) : NotFound();
        //}

        //[HttpDelete("DeleteDelivery")]
        //public Task<IActionResult> Delete(Delivery delivery) 
        //{
        //    bool isDeleted = _deliveryRepository.Delete(delivery);

        //    return isDeleted ? Ok() : NotFound();
        //}
    }

}

