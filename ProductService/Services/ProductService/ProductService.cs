using AutoMapper;
using s3ng.ProductService.Services.Abstractions;
using s3ng.ProductService.Services.Contracts.Product;
using s3ng.ProductService.Services.Repositories.Abstractions;
using s3ng.ProductService.Core.Domain.Managment;

namespace s3ng.ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Создать товар.
        /// </summary>
        /// <param name="creatingProductDto"> ДТО создаваемого товара. </param>
        public async Task<Guid> CreateAsync(CreatingProductDto creatingProductDto)
        {
            var product = _mapper.Map<Product>(creatingProductDto);
            var createdProduct = await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();

            return createdProduct.Id;
        }

        /// <summary>
        /// Получить товар по id.
        /// </summary>
        /// <param name="id"> Идентификатор товара. </param>
        /// <returns> ДТО товара.</returns>
        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id, CancellationToken.None);
            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        /// <summary>
        /// Изменить товар по id.
        /// </summary>
        /// <param name="id"> Иентификатор товара. </param>
        /// <param name="updatingProductDto"> ДТО редактируемого товара. </param>
        public async Task UpdateAsync(Guid id, UpdatingProductDto updatingProductDto)
        {
            var product = await _productRepository.GetAsync(id, CancellationToken.None);

            if (product is null) 
            {
                throw new Exception($"Товар с идентфикатором {id} не найден");
            }

            //????????????????????????????
            product.Description = updatingProductDto.Description;
            product.Price = updatingProductDto.Price;
            product.SellerId = updatingProductDto.SellerId;
            product.TimeModified = DateTime.Now;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить товар.
        /// </summary>
        /// <param name="id"> Идентификатор товара. </param>
        public async Task DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id, CancellationToken.None);
            product.IsDeleted = true;
        }
    }
}
