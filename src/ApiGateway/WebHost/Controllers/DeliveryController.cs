using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SharedLibrary.DeliveryService.Models;
using SharedLibrary.DeliveryService.Dto;
using System.Net;
using System.Text;
using System.Text.Json;

namespace WebHost.Controllers;

[ApiController]
[Route("[controller]")]
public class DeliveryController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("DeliveryService");

    [HttpGet("get/{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/api/delivery/{id}");

        string content = await response.Content.ReadAsStringAsync();

        return response.StatusCode switch
        {
            HttpStatusCode.OK => Ok(content),
            HttpStatusCode.NotFound => NotFound(),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpPut("Create")]
    public async Task<ActionResult> Create(CreateDeliveryDto createDeliveryDto)
    {
        StringContent content = new(System.Text.Json.JsonSerializer.Serialize(createDeliveryDto),
                                    Encoding.UTF8,
                                    "application/json");
        HttpResponseMessage response = await _httpClient.PutAsync($"/api/CreateDelivery", content);

        string result = await response.Content.ReadAsStringAsync();

        return response.StatusCode switch
        {
            HttpStatusCode.Created => Created("", result),
            HttpStatusCode.BadRequest => BadRequest(),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpPatch("Update")]
    public async Task<ActionResult> Update(UpdateDeliveryDto updateDeliveryDto)
    {
        StringContent content = new(System.Text.Json.JsonSerializer.Serialize(updateDeliveryDto),
                                    Encoding.UTF8,
                                    "application/json");
        HttpResponseMessage response = await _httpClient.PatchAsync($"/api/UpdateDelivery",
                                                            content);

        string responseContent = await response.Content.ReadAsStringAsync();

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.NotModified => StatusCode(304, responseContent),
            HttpStatusCode.InternalServerError => StatusCode(500, $"Failed to save the {nameof(Delivery)} to the database."),
            _ => throw new NotImplementedException(),
        };
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(Guid guid)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/DeleteDelivery/{guid}");

        return response.StatusCode switch
        {
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.InternalServerError => StatusCode(500, $"Failed to delete the {nameof(Delivery)} from the database."),
            _ => throw new NotImplementedException(),
        };
    }
}
