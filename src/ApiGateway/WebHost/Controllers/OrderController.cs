using System.Text;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.OrderService.Models;
using System.Net;
using SharedLibrary.OrderService.Dto;

namespace WebHost.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("OrderService");

    [HttpGet("get/{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/api/order/{id}");

        string content = await response.Content.ReadAsStringAsync();

        return response.StatusCode switch
        {
            HttpStatusCode.OK => Ok(content),
            HttpStatusCode.NotFound => NotFound(),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpPut("Create")]
    public async Task<ActionResult> Create(CreateOrderDto createOrderDto)
    {
        StringContent content = new(System.Text.Json.JsonSerializer.Serialize(createOrderDto),
                                    Encoding.UTF8,
                                    "application/json");
        HttpResponseMessage response = await _httpClient.PutAsync($"/api/CreateOrder", content);

        string result = await response.Content.ReadAsStringAsync();

        return response.StatusCode switch
        {   
            HttpStatusCode.Created => Created("", result),
            HttpStatusCode.BadRequest => BadRequest(),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpPatch("Update")]
    public async Task<ActionResult> Update(UpdateOrderDto updateOrderDto)
    {
        StringContent content = new(System.Text.Json.JsonSerializer.Serialize(updateOrderDto),
                                    Encoding.UTF8,
                                    "application/json");
        HttpResponseMessage response = await _httpClient.PatchAsync($"/api/UpdateOrder", 
                                                            content);

        string responseContent = await response.Content.ReadAsStringAsync();

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.NotModified => StatusCode(304, responseContent),
            HttpStatusCode.InternalServerError => StatusCode(500, $"Failed to save the {nameof(Order)} to the database."),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(Guid guid)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/DeleteOrder/{guid}");

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.InternalServerError => StatusCode(500, $"Failed to delete the {nameof(Order)} from the database."),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpGet("get-orders/user/{userId}")]
    public async Task<ActionResult<List<Order>>> GetOrdersByUser(Guid userId)
    {
        var response = await _httpClient.GetAsync($"/api/orders/user/{userId}");

        string content = await response.Content.ReadAsStringAsync();

        return response.StatusCode switch
        {
            HttpStatusCode.OK => Ok(content),
            HttpStatusCode.NotFound => NotFound(),
            _ => throw new NotImplementedException(),
        };
    }
}
