using Microsoft.AspNetCore.Mvc;
using SharedLibrary.ProductService.Models;
using System.Text;
using System.Text.Json;

namespace WebHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductService");
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/product/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, "application/json");
            }
            return NotFound();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(CreatingProductModel creatingProductModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(creatingProductModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"/api/create-product", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Created("", result);
            }
            return BadRequest();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdatingProductModel updatingProductModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(updatingProductModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/update-product/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"/api/delete-product/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
