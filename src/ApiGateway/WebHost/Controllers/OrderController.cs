using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.OrderService.Models;
using System.Net;

namespace WebHost.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("OrderService");

    [HttpGet("Get/{guid}")]
    public async Task<ActionResult> Get(Guid guid)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/api/Order/{guid}");

        return response.StatusCode switch
        {
            HttpStatusCode.OK => Ok(response.Content.ReadAsStringAsync()),
            HttpStatusCode.NotFound => NotFound(),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpPut("Create")]
    public async Task<ActionResult> Create(Order order)
    {
        StringContent content = new(JsonSerializer.Serialize(order),
                                    Encoding.UTF8,
                                    "application/json");
        HttpResponseMessage response = await _httpClient.PutAsync($"/api/CreateOrder",
                                                                  content);

        return response.StatusCode switch
        {
            HttpStatusCode.Created => Created(),
            HttpStatusCode.NoContent => NoContent(),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpPatch("Update")]
    public async Task<ActionResult> Update(Order order)
    {
        StringContent content = new(JsonSerializer.Serialize(order),
                                    Encoding.UTF8,
                                    "application/json");
        HttpResponseMessage response = await _httpClient.PutAsync($"/api/UpdateOrder",
                                                                  content);

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.OK => NoContent(),
            HttpStatusCode.InternalServerError => StatusCode(500, $"Failed to save the {nameof(Order)} to the database."),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(Guid guid)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/DeleteOrder");

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.OK => NoContent(),
            HttpStatusCode.InternalServerError => StatusCode(500, $"Failed to delete the {nameof(Order)} to the database."),
            _ => throw new NotImplementedException(),
        };
    }
}
