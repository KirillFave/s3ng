using Microsoft.AspNetCore.Mvc;
using Refit;
using SharedLibrary.ProductService.Contracts;
using SharedLibrary.ProductService.Dto;

namespace WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IProductServiceClient productServiceClient) : ControllerBase
    {
        private readonly IProductServiceClient _productServiceClient = productServiceClient;

        // GET /api/gateway/products
        [HttpGet]
        public async Task<ActionResult<List<ProductResponseDto>>> GetAllProducts()
        {
            try
            {
                var products = await _productServiceClient.GetAllAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching products: {ex.Message}");
            }
        }

        // GET /api/gateway/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(string id)
        {
            try
            {
                var product = await _productServiceClient.GetByIdAsync(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching product with id {id}: {ex.Message}");
            }
        }

        // POST /api/gateway/products
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<ActionResult<string>> CreateProduct([FromForm] ProductCreateDto dto)
        {
            try
            {
                // Преобразуем IFormFile в StreamPart для изображения
                StreamPart? imagePart = null;
                if (dto.Image != null)
                {
                    imagePart = new StreamPart(dto.Image.OpenReadStream(), dto.Image.FileName, dto.Image.ContentType);
                }

                // Вызов метода через Refit с DTO
                var result = await _productServiceClient.CreateAsync(dto.Name, dto.Description, dto.Price, imagePart);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating product: {ex.Message}");
            }
        }

        // PUT /api/gateway/products/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> UpdateProduct(string id, [FromForm] ProductUpdateDto dto)
        {
            try
            {
                // Преобразуем IFormFile в StreamPart для изображения
                StreamPart? imagePart = null;
                if (dto.Image != null)
                {
                    imagePart = new StreamPart(dto.Image.OpenReadStream(), dto.Image.FileName, dto.Image.ContentType);
                }

                // Вызов метода через Refit с DTO
                await _productServiceClient.UpdateAsync(id, dto.Name, dto.Description, dto.Price, imagePart);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating product with id {id}: {ex.Message}");
            }
        }

        // DELETE /api/gateway/products/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                // Вызов метода через Refit для удаления товара
                await _productServiceClient.DeleteAsync(id);

                return NoContent(); // No Content for successful DELETE
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting product with id {id}: {ex.Message}");
            }
        }
    }
}
