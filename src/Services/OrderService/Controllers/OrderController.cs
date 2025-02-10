using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Repositories;
using SharedLibrary.OrderService.Dto;
using SharedLibrary.OrderService.Models;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly OrderRepository _orderRepository;

    public OrderController(OrderRepository orderRepository, IMapper mapper)
    {
        _mapper = mapper;
        _orderRepository = orderRepository;
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

        bool result = await _orderRepository.AddAsync(order);

        return result ? Created("", order.Id) : BadRequest();
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

    [HttpPost("/api/DeleteOrderItem")]
    public async Task<ActionResult> DeleteOrderItem(Guid orderGuid, Guid orderItemGuid)
    {
        Order? order = await _orderRepository.GetByIdAsync(orderGuid);

        if (order == null)
        {
            return NotFound();
        }

        OrderItem? orderItemToRemove = order.Items.SingleOrDefault(x => x.Id == orderItemGuid);

        if (orderItemToRemove is null)
        {
            return NotFound();
        }

        order.Items.Remove(orderItemToRemove);

        OperationResult result = await _orderRepository.DeleteOrderItemAsync(order);

        return result switch
        {
            OperationResult.NotEntityFound => NotFound(),
            OperationResult.Success => NoContent(),
            OperationResult.NotModified => StatusCode(304, $"{nameof(Order)} not modified."),
            OperationResult.NotChangesApplied => StatusCode(500),
            _ => throw new NotImplementedException(),
        };
    }
}
