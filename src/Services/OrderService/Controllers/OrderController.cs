using Microsoft.AspNetCore.Mvc;
using OrderService.Repositories;
using SharedLibrary.OrderService.Models;

namespace OrderService.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : Controller
{
    private readonly OrderRepository _orderRepository;

    public OrderController(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("api/Order/{guid}")]
    public async Task<ActionResult> Get(Guid guid)
    {
        Order? order = await _orderRepository.GetByIdAsync(guid);

        return order is null ? NotFound() : Ok(order);
    }

    [HttpPut("api/CreateOrder")]
    public async Task<ActionResult> Create(Order order)
    {
        bool result = await _orderRepository.AddAsync(order);

        return result ? NoContent() : Created();
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
