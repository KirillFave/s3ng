using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Repositories;
using OrderService.Producers;
using SharedLibrary.Common.Kafka.Messages;
using SharedLibrary.OrderService.Dto;
using SharedLibrary.OrderService.Models;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly OrderRepository _orderRepository;
    private readonly OrderCreatedProducer _orderCreatedProducer;
    private readonly OrderCanceledProducer _orderCanceledProducer;

    public OrderController(
        OrderRepository orderRepository,
        IMapper mapper,
        OrderCreatedProducer orderCreatedProducer,
        OrderCanceledProducer orderCanceledProducer)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
        _orderCreatedProducer = orderCreatedProducer;
        _orderCanceledProducer = orderCanceledProducer;
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
                new OrderCreatedMessage()
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

    [HttpPatch("/api/order/cancel/{id}")]
    public async Task<ActionResult> Cancel(Guid id) 
    {
        OperationResult result = await _orderRepository.CancelAsync(id);

        if (result is OperationResult.Success)
        {
            await _orderCanceledProducer.ProduceAsync(
                id.ToString(),
                new OrderCanceledMessage()
                {
                    Id = id
                }
            );

            return NoContent();
        }

        return result switch
        {
            OperationResult.NotEntityFound => NotFound(),
            OperationResult.NotModified => StatusCode(304, $"{nameof(Order)} not modified."),
            OperationResult.NotChangesApplied => StatusCode(500, $"Failed to save the {nameof(Order)} to the database."),
            _ => throw new NotImplementedException(),
        };
    }
}
