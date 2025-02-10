using Microsoft.AspNetCore.Mvc;
using SharedLibrary.OrderService.Dto;
using SharedLibrary.OrderService.Models;
using System.Net;
using System.Text;

namespace WebHost.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("OrderService");

    [HttpGet("get/{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/api/orderitem/{id}");

        string content = await response.Content.ReadAsStringAsync();

        return response.StatusCode switch
        {
            HttpStatusCode.OK => Ok(content),
            HttpStatusCode.NotFound => NotFound(),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpPut("Create")]
    public async Task<ActionResult> Create(CreateOrderItemDto createOrderItemDto)
    {
        StringContent content = new(System.Text.Json.JsonSerializer.Serialize(createOrderItemDto),
                                    Encoding.UTF8,
                                    "application/json");
        HttpResponseMessage response = await _httpClient.PutAsync($"/api/CreateOrderItem", content);

        string result = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.Created)
        { 
            content = new()
        }

        return response.StatusCode switch
        {
            HttpStatusCode.Created => Created("", result),
            HttpStatusCode.BadRequest => BadRequest(),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(Guid guid)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/DeleteOrderItem/{guid}");

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.OK => NoContent(),
            HttpStatusCode.InternalServerError => StatusCode(500, $"Failed to delete the {nameof(OrderItem)} from the database."),
            _ => throw new NotImplementedException(),
        };
    }
}
