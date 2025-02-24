using AutoMapper;
using DeliveryService.Delivery.BusinessLogic.Models;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;
using DeliveryService.Delivery.Core.Models.Requests;
using DeliveryService.Delivery.Core.Models.Responses;
using DeliveryService.Delivery.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Delivery.Core.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class DeliveryController( IDeliveryService            _deliveryService,
                                     IMapper                     _mapper,
                                     ILogger<DeliveryController> _logger) : ControllerBase 
    {   /// <summary>
        /// Получение доставки через Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/delivery/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var deliveryModel = _mapper.Map<DeliveryModel>(await _deliveryService.GetByIdAsync(id));
            return Ok(_mapper.Map<DeliveryDBContext>(await _deliveryService.GetByIdAsync(id)));

        }
        /// <summary>
        /// Создание доставки
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("/api/create-delivery")]             
        public async Task<ActionResult<DeliveryResponse>> CreateAsync(CreateDeliveryRequest request)
        {            
            var delivery = await _deliveryService.CreateAsync(_mapper.Map<CreateDeliveryDto>(request));
            return Ok(_mapper.Map<DeliveryResponse>(delivery));        }

        /// <summary>
        /// Изменение доставки по Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("/api/update-delivery/{id}")]
        public async Task<ActionResult<EditDeliveryResponse>> TryUpdateAsync(Guid id, EditDeliveryRequest request)
        { 
            var delivery = await _deliveryService.TryUpdateAsync(id, _mapper.Map<EditDeliveryRequest, UpdateDeliveryDto>(request));            
            return Ok(_mapper.Map<EditDeliveryResponse>(delivery));
        }

        [HttpDelete("/api/delete-delivery/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            bool isDeleted = await _deliveryService.TryDeleteAsync(id);
            return isDeleted ? Ok(id) : NotFound();
        }
    }
}