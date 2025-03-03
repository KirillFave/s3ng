using System.Text.Json;
using System.Threading;
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
    public class DeliveryController(IDeliveryService _deliveryService,
                                    IMapper _mapper,
                                    ILogger<DeliveryController> _logger) : ControllerBase
    {
        /// <summary>
        /// Получение доставки через Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [HttpGet("/api/delivery/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var deliveryModel = _mapper.Map<DeliveryDto>(await _deliveryService.GetByIdAsync(id, cancellationToken));
            return Ok(_mapper.Map<DeliveryDBContext>(await _deliveryService.GetByIdAsync(id, cancellationToken)));

        }
        /// <summary>
        /// Создание доставки
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("/api/create-delivery")]
        public async Task<ActionResult<CreateDeliveryResponse>> CreateAsync(CreateDeliveryRequest request, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryService.CreateAsync(_mapper.Map<CreateDeliveryDto>(request), cancellationToken);
            return Ok(_mapper.Map<CreateDeliveryResponse>(delivery));
        }

        /// <summary>
        /// Изменение доставки по Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("/api/update-delivery/{id}")]
        public async Task<ActionResult<EditDeliveryResponse>> UpdateAsync(Guid id, EditDeliveryRequest request, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryService.UpdateAsync(id, _mapper.Map<EditDeliveryRequest, UpdateDeliveryDto>(request), cancellationToken);
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
        /// Получение статуса доставки через Guid заказа
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("/api/GetDeliveryStatus/{orderId}")]
        public async Task<IActionResult> GetDeliveryStatus(Guid orderId, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryService.GetByIdAsync(orderId, cancellationToken);
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
        //[HttpGet("/api/getDeliveryStatusByOrderId/{orderId}")]
        //public async Task<IActionResult> GetDeliveryStatusByOrderId(Guid orderId, CancellationToken cancellationToken)
        //{
        //    var delivery = await _deliveryService.GetDeliveryByOrderIdAsync(orderId, cancellationToken);
        //    if (orderId != null)
        //        return Ok(JsonSerializer.Serialize(delivery));
        //}
    }
}

