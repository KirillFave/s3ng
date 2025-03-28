using System;
using AutoMapper;
using System.Text.Json;
using System.Threading;
using DeliveryService.Delivery.BusinessLogic.Models;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;
using DeliveryService.Delivery.Core.Cache;
using DeliveryService.Delivery.Core.Models.Requests;
using DeliveryService.Delivery.Core.Models.Responses;
using DeliveryService.Delivery.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace DeliveryService.Delivery.Core.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class DeliveryController(IDeliveryService _deliveryService,                                   
                                    IMapper _mapper,
                                    IDistributedCache _distributedCache) : ControllerBase
    {       

        /// <summary>
        /// Получение доставки через Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>         
        [HttpGet("/api/delivery/{id}")]
        [ProducesResponseType<GetDeliveryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetDeliveryResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            string? serialized = await _distributedCache.GetStringAsync(CacheKeys.DeliveryKey(id), HttpContext.RequestAborted);

            if (serialized is not null)
            {
                var cachResult = JsonSerializer.Deserialize<IEnumerable<GetDeliveryResponse>>(serialized);

                if (cachResult is not null)
                {
                    return Ok(cachResult);
                }
            }
            var delivery = _mapper.Map<GetDeliveryResponse>(await _deliveryService.GetByIdAsync(id, cancellationToken));
            return Ok(delivery);
        }
        /// <summary>
        /// Создание доставки
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("/api/create-delivery")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            bool isDeleted = await _deliveryService.TryDeleteAsync(id, cancellationToken);
            return isDeleted ? Ok(id) : NotFound();
        }

        /// <summary>
        /// Получение статуса доставки по Guid заказа
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("/api/GetDeliveryStatus/{orderId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetDeliveryStatus(Guid orderId, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryService.GetByIdAsync(orderId, cancellationToken);
            if (delivery != null)
                return Ok(JsonSerializer.Serialize(delivery));

            return NotFound();
        }       
    }
}


