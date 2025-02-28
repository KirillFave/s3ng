using System.Text.Json;
using AutoMapper;
using DeliveryService.Delivery.BusinessLogic.Models;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;
using DeliveryService.Delivery.Core.Models.Requests;
using DeliveryService.Delivery.Core.Models.Responses;
using DeliveryService.Delivery.DataAccess.Abstractions;
using DeliveryService.Delivery.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Delivery.Core.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class DeliveryController( IDeliveryService            _deliveryService,
                                     IDeliveryRepository         _deliveryRepository,   
                                     IMapper                     _mapper,
                                     ILogger<DeliveryController> _logger) : ControllerBase 
    {   
        /// <summary>
        /// Получение доставки через Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
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

        /// <summary>
        /// Удаление доставки по Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("/api/delete-delivery/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            bool isDeleted = await _deliveryService.TryDeleteAsync(id, cancellationToken);
            return isDeleted ? Ok(id) : NotFound();
        }

        /// <summary>
        /// Получение статуса доставки через Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/getDeliveryStatus/{id}")]
        public async Task<IActionResult> GetDeliveryStatus(Guid id, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryRepository.GetAsync(id, cancellationToken);
            if (delivery != null)
                return Ok(JsonSerializer.Serialize(delivery));

            return NotFound();
        }
        /// <summary>
        /// Получение статуса доставки через id заказа 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// 
        //[HttpGet("/api/getDeliveryStatusByOrderId/{}")]
        //public async Task<IActionResult> GetDeliveryStatusByOrderId(Guid orderId)
        //{
        //    var delivery = await _deliveryRepository.GetDeliveryByOrderIdAsync(orderId);
        //    if (
    }
}
