using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.Services.Abstractions;
using SharedLibrary.ProductService.Models;
using ILogger = Serilog.ILogger;

namespace ProductService.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ProductController(IProductService service, IMapper mapper, ILogger logger)
        {
            _productService = service;
            _mapper = mapper;
            _logger = logger.ForContext<ProductController>();
        }

        [HttpGet("/api/product/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            _logger.Information($"Пришёл http запрос GetAsync для id={id}");
            var productModel = _mapper.Map<ProductModel>(await _productService.GetByIdAsync(id));
            return productModel is null ? NotFound() : Ok(productModel);
        }

        [HttpPost("/api/create-product")]
        public async Task<IActionResult> CreateAsync(CreatingProductModel creatingProductModel)
        {
            _logger.Information($"Пришёл http запрос CreateAsync для creatingProductModel={creatingProductModel}");
            var createdProductGuid = await _productService.CreateAsync(creatingProductModel);
            return Created("", createdProductGuid);
        }

        [HttpPut("/api/update-product/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdatingProductModel updatingProductModel)
        {
            _logger.Information($"Пришёл http запрос UpdateAsync для updatingProductModel={updatingProductModel}");
            bool isUpdated = await _productService.TryUpdateAsync(id, updatingProductModel);
            return isUpdated ? Ok() : NotFound($"Продукт с {id} не найден");
        }

        [HttpDelete("/api/delete-product/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.Information($"Пришёл http запрос DeleteAsync для id={id}");
            bool isDeleted = await _productService.TryDeleteAsync(id);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
