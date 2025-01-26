using Microsoft.AspNetCore.Mvc;
using OrderService.Repositories;
using SharedLibrary.OrderService.Models;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderRepository _orderRepository;

    public OrderController(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("/api/order/{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        Order? order = await _orderRepository.GetByIdAsync(id);

        return order is null ? NotFound() : Ok(order);
    }

    [HttpPut("/api/CreateOrder")]
    public async Task<ActionResult> Create(Order order)
    {
        bool result = await _orderRepository.AddAsync(order);

        return result ? NoContent() : BadRequest();
    }

    [HttpPatch("api/UpdateOrder")]
    public async Task<ActionResult> Update(Order order)
    {
        OperationResult result = await _orderRepository.UpdateAsync(order, false);

        return result switch
        {
            OperationResult.NotEntityFound => NotFound(),
            OperationResult.Success => NoContent(),
            OperationResult.NotChangesApplied => StatusCode(500, $"Failed to save the {nameof(Order)} to the database."),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpDelete("api/DeleteOrder")]
    public async Task<ActionResult> Delete(Guid guid)
    {
        OperationResult result = await _orderRepository.DeleteAsync(guid);

        return result switch
        {
            OperationResult.NotEntityFound => NotFound(),
            OperationResult.Success => NoContent(),
            OperationResult.NotChangesApplied => StatusCode(500, $"Failed to delete the {nameof(Order)} to the database."),
            _ => throw new NotImplementedException(),
        };
    }
}
