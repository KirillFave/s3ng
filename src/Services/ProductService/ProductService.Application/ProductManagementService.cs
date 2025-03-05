using AutoMapper;
using ProductService.Domain;
using SharedLibrary.ProductService.Dto;

namespace ProductService.Application
{
    public class ProductManagementService(IProductRepository repository, IFileStorage fileStorage, IMapper mapper) : IProductManagementService
    {
        private readonly IProductRepository _repository = repository;
        private readonly IFileStorage _fileStorage = fileStorage;
        private readonly IMapper _mapper = mapper;

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

        public async Task UpdateAsync(string id, ProductUpdateDto dto, CancellationToken ct = default)
        {
            var product = await _repository.GetByIdAsync(id, ct);
            if (product is null)
                return;

            //Создаем сущность нового продукта, отдаем ей айдишник и присваиваем старую (возможную) картинку
            var newProduct = _mapper.Map<Product>(dto);
            newProduct.Id = id;
            newProduct.ImageUrl = product.ImageUrl;

            //Если в dto передана картинка
            if (dto.Image != null)
            {
                //В БД уже есть УРЛ старой картинки
                if (!string.IsNullOrEmpty(product.ImageUrl))
                    //Чистим старую
                    await _fileStorage.DeleteFileAsync(product.ImageUrl);

                //Добавляем новую
                newProduct.ImageUrl = await _fileStorage.UploadFileAsync(dto.Image, ct);
            }

            await _repository.UpdateAsync(newProduct, ct);
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
