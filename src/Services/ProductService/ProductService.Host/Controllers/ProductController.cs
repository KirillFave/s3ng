using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using s3ng.ProductService.Services.Abstractions;
using s3ng.ProductService.Services.Contracts.Product;
using s3ng.ProductService.Host.Models.Product;

namespace ProductService.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService service, IMapper mapper)
        {
            _productService = service;
            _mapper = mapper;
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var productModel = _mapper.Map<ProductModel>(await _productService.GetByIdAsync(id));

            return productModel is null ? NotFound() : Ok(productModel);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateAsync(CreatingProductModel creatingProductModel)
        {
            var creatingProductDto = _mapper.Map<CreatingProductDto>(creatingProductModel);
            var createdProductGuid = await _productService.CreateAsync(creatingProductDto);

            return Created("", createdProductGuid);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdatingProductModel updatingProductModel)
        {
            var updatingProductDto = _mapper.Map<UpdatingProductDto>(updatingProductModel);
            bool isUpdated = await _productService.TryUpdateAsync(id, updatingProductDto);

            return isUpdated ? Ok() : NotFound($"Товар с идентфикатором {id} не найден");
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            bool isDeleted = await _productService.TryDeleteAsync(id);

            return isDeleted ? Ok() : NotFound();
        }
    }
}
