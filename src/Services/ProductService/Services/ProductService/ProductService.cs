using AutoMapper;
using ProductService.Services.Abstractions;
using ProductService.Services.Repositories.Abstractions;
using ProductService.Core.Domain.Managment;
using SharedLibrary.ProductService.Models;

namespace ProductService.Services
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
        public async Task<Guid> CreateAsync(CreatingProductModel creatingProductDto)
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
        public async Task<ProductModel> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id, CancellationToken.None);
            var productDto = _mapper.Map<ProductModel>(product);

            return productDto;
        }

        /// <summary>
        /// Изменить товар по id.
        /// </summary>
        /// <param name="id"> Иентификатор товара. </param>
        /// <param name="updatingProductDto"> ДТО редактируемого товара. </param>
        public async Task<bool> TryUpdateAsync(Guid id, UpdatingProductModel updatingProductDto)
        {
            var product = await _productRepository.GetAsync(id, CancellationToken.None);

            if (product is null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(updatingProductDto.Description))
            {
                product.Description = updatingProductDto.Description;
            }

            if (updatingProductDto.Price is not null)
            {
                product.Price = updatingProductDto.Price.Value;
            }
            
            product.SellerId = updatingProductDto.SellerId;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Удалить товар.
        /// </summary>
        /// <param name="id"> Идентификатор товара. </param>
        public async Task<bool> TryDeleteAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id, CancellationToken.None);

            if (product is null)
            {
                return false;
            }

            product.IsDeleted = true;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            return true;
        }
    }
}
