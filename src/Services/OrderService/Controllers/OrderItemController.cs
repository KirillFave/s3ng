using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Dto;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderItemController : Controller
{
    private readonly OrderRepository _orderRepository;
    private readonly OrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public OrderItemController(
        OrderRepository orderRepository,
        OrderItemRepository orderItemRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> Get(Guid guid)
    {
        OrderItem? orderItem = await _orderItemRepository.GetByIdAsync(guid);

        if (orderItem is null) return NotFound();

        var responseDto = _mapper.Map<GetOrderItemResponseDto>(orderItem);

        return Ok(responseDto);
    }

    [HttpPut]
    public async Task<ActionResult> Create(CreateOrderItemDto dto)
    {
        Order? order = await _orderRepository.GetByIdAsync(dto.OrderGuid);

        if (order == null)
        {
            return NotFound("Order not found.");
        }

        OrderItem orderItem = _mapper.Map<OrderItem>(dto);
        orderItem.Order = order;

        bool result = await _orderItemRepository.AddAsync(orderItem);

        if (!result) return NoContent();

        var responseDto = _mapper.Map<GetOrderItemResponseDto>(orderItem);

        return Created($"api/v1/OrderItem/{responseDto.Guid}", responseDto);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Guid guid)
    {
        OperationResult result = await _orderItemRepository.DeleteAsync(guid);

        return result switch
        {
            OperationResult.NotEntityFound => NotFound(),
            OperationResult.Success => NoContent(),
            OperationResult.NotChangesApplied => StatusCode(500, $"Failed to delete the {nameof(OrderItem)} from the database."),
            _ => throw new NotImplementedException(),
        };
    }
}
