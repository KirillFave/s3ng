using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductService");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Content(content, response.Content.Headers.ContentType?.ToString());
            }
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
