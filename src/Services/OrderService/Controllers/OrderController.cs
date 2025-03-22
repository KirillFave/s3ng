using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Repositories;
using OrderService.Producers;
using SharedLibrary.Common.Kafka.Messages;
using SharedLibrary.OrderService.Dto;
using SharedLibrary.OrderService.Models;
using SharedLibrary.ProductService.Dto;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly OrderRepository _orderRepository;
    private readonly OrderCreatedProducer _orderCreatedProducer;

    public OrderController(OrderRepository orderRepository, IMapper mapper, OrderCreatedProducer orderCreatedProducer)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _orderCreatedProducer = orderCreatedProducer;
    }

    [HttpGet("/api/order/{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        Order? order = await _orderRepository.GetByIdAsync(id);

        if (order == null) 
        { 
            return NotFound();
        }

        GetOrderResponseDto getOrderResponseDto = _mapper.Map<GetOrderResponseDto>(order);

        return Ok(getOrderResponseDto);
    }

    [HttpPut("/api/CreateOrder")]
    public async Task<ActionResult> Create(CreateOrderDto createOrderDto)
    {
        Order order = _mapper.Map<Order>(createOrderDto);
        order.Id = Guid.NewGuid();

        if (order.Items is not null)
        {
            foreach(OrderItem item in order.Items)
            {
                item.OrderId = order.Id;
            }
        }

        bool result = await _orderRepository.AddAsync(order);

        if (result)
        {
            ActionResult response = await Get(order.Id);

            GetOrderResponseDto getOrderResponseDto;

            if (response is OkObjectResult okResult)
            {
                getOrderResponseDto = okResult.Value as GetOrderResponseDto;
            }
            else
            {
                throw new NotImplementedException();
            }
            
            await _orderCreatedProducer.ProduceAsync(
                order.Id.ToString(),
                new OrderCreatedMessage
                {
                    Id = getOrderResponseDto!.Id,
                    UserGuid = getOrderResponseDto!.UserGuid,
                    Items = getOrderResponseDto!.Items,
                    Status = getOrderResponseDto!.Status,
                    PaymentType = getOrderResponseDto!.PaymentType,
                    ShipAddress = getOrderResponseDto!.ShipAddress,
                    CreatedTimestamp = getOrderResponseDto!.CreatedTimestamp
                }
            );
            return Created("", order.Id);
        }

        return BadRequest();
    }

    [HttpPatch("/api/UpdateOrder")]
    public async Task<ActionResult> Update(UpdateOrderDto updateOrderDto)
    {
        Order order = _mapper.Map<Order>(updateOrderDto);

        OperationResult result = await _orderRepository.UpdateAsync(order);

        return result switch
        {
            OperationResult.NotEntityFound => NotFound(),
            OperationResult.Success => NoContent(),
            OperationResult.NotModified => StatusCode(304, $"{nameof(Order)} not modified."),
            OperationResult.NotChangesApplied => StatusCode(500, $"Failed to save the {nameof(Order)} to the database."),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpDelete("/api/DeleteOrder/{guid}")]
    public async Task<ActionResult> Delete(Guid guid)
    {
        OperationResult result = await _orderRepository.DeleteAsync(guid);

        return result switch
        {
            OperationResult.NotEntityFound => NotFound(),
            OperationResult.Success => NoContent(),
            OperationResult.NotChangesApplied => StatusCode(500, $"Failed to delete the {nameof(Order)} from the database."),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpGet("/api/orders/user/{userId}")]
    public async Task<ActionResult<List<Order>>> GetOrdersByUser(Guid userId)
    {
        var orders = await _orderRepository.GetOrdersByUserAsync(userId);
        return Ok(orders);
    }
}
