using AutoMapper;
using ProductService.Domain;
using SharedLibrary.ProductService.Dto;

namespace ProductService.Application
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IProductRepository _repository;
        private readonly IFileStorage _fileStorage;
        private readonly IMapper _mapper;

        public ProductManagementService(IProductRepository repository, IFileStorage fileStorage, IMapper mapper)
        {
            _repository = repository;
            _fileStorage = fileStorage;
            _mapper = mapper;
        }

        public Task<List<Product>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Product?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);

        public async Task<string> CreateAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            product.Id = Guid.NewGuid().ToString();
            if (dto.Image != null)
            {
                product.ImageUrl = await _fileStorage.UploadFileAsync(dto.Image);
            }

            await _repository.CreateAsync(product);
            return product.Id;
        }

        public async Task<string> UpdateAsync(ProductUpdateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            if (dto.Image != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    await _fileStorage.DeleteFileAsync(product.ImageUrl);
                    product.ImageUrl = string.Empty;
                }
                product.ImageUrl = await _fileStorage.UploadFileAsync(dto.Image);
            }
            await _repository.UpdateAsync(product);
            return product.Id;
        }

        public async Task DeleteAsync(string id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product is null)
                return;

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                await _fileStorage.DeleteFileAsync(product.ImageUrl);
            }
            await _repository.DeleteAsync(id);
        }
    }
}
