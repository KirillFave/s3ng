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
        public async Task<ProductModel> GetAsync(Guid id)
        {
            var productModel = _mapper.Map<ProductModel>(await _productService.GetByIdAsync(id));

            return productModel;
        }

        [HttpPost("CreateProduct")]
        public async Task<Guid> CreateAsync(CreatingProductModel creatingProductModel)
        {
            var creatingProductDto = _mapper.Map<CreatingProductDto>(creatingProductModel);
            var createdProductGuid = await _productService.CreateAsync(creatingProductDto);

            return createdProductGuid;
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdatingProductModel updatingProductModel)
        {
            var updatingProductDto = _mapper.Map<UpdatingProductDto>(updatingProductModel);
            await _productService.UpdateAsync(id, updatingProductDto);

            return Ok();
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _productService.DeleteAsync(id);

            return Ok();
        }
    }
}
