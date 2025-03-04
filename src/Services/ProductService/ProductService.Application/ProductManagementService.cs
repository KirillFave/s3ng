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

        public Task<List<Product>> GetAllAsync(CancellationToken ct = default) => _repository.GetAllAsync(ct);

        public Task<Product?> GetByIdAsync(string id, CancellationToken ct = default) => _repository.GetByIdAsync(id, ct);

        public async Task<string> CreateAsync(ProductCreateDto dto, CancellationToken ct = default)
        {
            var product = _mapper.Map<Product>(dto);
            product.Id = Guid.NewGuid().ToString();
            if (dto.Image != null)
            {
                product.ImageUrl = await _fileStorage.UploadFileAsync(dto.Image, ct);
            }

            await _repository.CreateAsync(product, ct);
            return product.Id;
        }

        public async Task<string> UpdateAsync(ProductUpdateDto dto, CancellationToken ct = default)
        {
            var product = _mapper.Map<Product>(dto);
            if (dto.Image != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    await _fileStorage.DeleteFileAsync(product.ImageUrl);
                    product.ImageUrl = string.Empty;
                }
                product.ImageUrl = await _fileStorage.UploadFileAsync(dto.Image, ct);
            }
            await _repository.UpdateAsync(product, ct);
            return product.Id;
        }

        public async Task DeleteAsync(string id, CancellationToken ct = default)
        {
            var product = await _repository.GetByIdAsync(id, ct);
            if (product is null)    
                return;

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                await _fileStorage.DeleteFileAsync(product.ImageUrl, ct);
            }
            await _repository.DeleteAsync(id, ct);
        }
    }
}
