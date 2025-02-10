using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.Repositories;
using SharedLibrary.OrderService.Dto;
using SharedLibrary.OrderService.Models;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
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

    [HttpGet("api/orderitem/{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        OrderItem? orderItem = await _orderItemRepository.GetByIdAsync(id);

        if (orderItem is null)
        {
            return NotFound();
        }

        GetOrderItemResponseDto getOrderItemResponseDto = _mapper.Map<GetOrderItemResponseDto>(orderItem);

        return Ok(getOrderItemResponseDto);
    }

    [HttpPut("/api/CreateOrderItem")]
    public async Task<ActionResult> Create(CreateOrderItemDto createOrderItemDto)
    {
        OrderItem orderItem = _mapper.Map<OrderItem>(createOrderItemDto);
        orderItem.Id = Guid.NewGuid();

        bool result = await _orderItemRepository.AddAsync(orderItem);

        return result ? Created("", orderItem.Id) : BadRequest();
    }

    [HttpDelete("/api/DeleteOrderItem/{guid}")]
    public async Task<ActionResult> Delete(Guid guid)
    {
        OperationResult result = await _orderItemRepository.DeleteAsync(guid);

        // TODO: Сделать удаление из Order  

        return result switch
        {
            OperationResult.NotEntityFound => NotFound(),
            OperationResult.Success => NoContent(),
            OperationResult.NotChangesApplied => StatusCode(500, $"Failed to delete the {nameof(OrderItem)} from the database."),
            _ => throw new NotImplementedException(),
        };
    }
}
