using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Database;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : Controller
{
    private readonly IRepository<Order> _orderRepository;

    public OrderController(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public async Task<ActionResult> Get(Guid guid)
    {
        Order? order = await _orderRepository.GetByIdAsync(guid);

        return order is null ? NotFound() : Ok(order);
    }

    [HttpPut]
    public async Task<ActionResult> Create(Order order)
    {
        bool result = await _orderRepository.AddAsync(order);

        return result ? NoContent() : Created();
    }

    [HttpPatch]
    public async Task<ActionResult> Update(Order order)
    {
        OperationResult result = await _orderRepository.UpdateAsync(order);

        return result switch
        {
            OperationResult.NotEntityFound => NotFound(),
            OperationResult.Success => NoContent(),
            OperationResult.NotChangesApplied => StatusCode(500, $"Failed to save the {nameof(Order)} to the database."),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpDelete]
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
