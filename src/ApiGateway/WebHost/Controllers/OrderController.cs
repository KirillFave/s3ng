using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.OrderService.Models;
using SharedLibrary.ProductService.Models;

namespace WebHost.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : Controller
{
    private readonly HttpClient _httpClient;

    public OrderController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("OrderService");
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult> Get(Guid guid)
    {
        var response = await _httpClient.GetAsync($"/api/v1/order/{guid}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        return NotFound();
    }

    [HttpPut]
    public async Task<ActionResult> Create(Order order)
    {
        var content = new StringContent(JsonSerializer.Serialize(order),
                                        Encoding.UTF8,
                                        "application/json");
        var response = await _httpClient.PutAsync($"/api/v1/create-order",
                                                   content);

        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            return Created("", result);
        }
        return NoContent();
    }

    [HttpPatch]
    public async Task<ActionResult> Update(Order order)
    {
        //OperationResult result = await _orderRepository.UpdateAsync(order, false);

        //return result switch
        //{
        //    OperationResult.NotEntityFound => NotFound(),
        //    OperationResult.Success => NoContent(),
        //    OperationResult.NotChangesApplied => StatusCode(500, $"Failed to save the {nameof(Order)} to the database."),
        //    _ => throw new NotImplementedException(),
        //};

        var content = new StringContent(JsonSerializer.Serialize(order),
                                        Encoding.UTF8,
                                        "application/json");
        var response = await _httpClient.PutAsync($"/api/v1/update-order",
                                                  content);

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(Guid guid)
    {
        //OperationResult result = await _orderRepository.DeleteAsync(guid);

        //return result switch
        //{
        //    OperationResult.NotEntityFound => NotFound(),
        //    OperationResult.Success => NoContent(),
        //    OperationResult.NotChangesApplied => StatusCode(500, $"Failed to delete the {nameof(Order)} to the database."),
        //    _ => throw new NotImplementedException(),
        //};

        HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/v1/delete-order/{guid}");

        if (response.IsSuccessStatusCode)
        {
            return Ok();
        }
        return NotFound();
    }
}
