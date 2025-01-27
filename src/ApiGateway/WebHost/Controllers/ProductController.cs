using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.IAM.Enums;
using SharedLibrary.ProductService.Models;
using ILogger = Serilog.ILogger;

namespace WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = nameof(RoleType.Admin))]
    public class ProductController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public ProductController(IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _httpClient = httpClientFactory.CreateClient("ProductService");
            _logger = logger.ForContext<ProductController>();
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            _logger.Information($"Api method GetAsync was called with parameters {id}");
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
            _logger.Information($"Api method CreateAsync was called with creatingProductModel: {creatingProductModel}");
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
            _logger.Information($"Api method UpdateAsync was called");
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
            _logger.Information($"Api method DeleteAsync was called with parameters {id}");
            var response = await _httpClient.DeleteAsync($"/api/delete-product/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
