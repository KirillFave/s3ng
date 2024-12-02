using s3ng.ProductService.Services.Contracts.Product;

namespace s3ng.ProductService.Services.Abstractions
{
    public interface IProductService
    {
        /// <summary>
        /// Создать товар.
        /// </summary>
        /// <param name="creatingProductDto"> ДТО создаваемого товара. </param>
        public Task<Guid> CreateAsync(CreatingProductDto creatingProductDto);

        /// <summary>
        /// Получить товар по id.
        /// </summary>
        /// <param name="id"> Идентификатор товара. </param>
        /// <returns> ДТО товара.</returns>
        public Task<ProductDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Изменить товар по id.
        /// </summary>
        /// <param name="id"> Иентификатор товара. </param>
        /// <param name="updatingProductDto"> ДТО редактируемого товара. </param>
        public Task<bool> TryUpdateAsync(Guid id, UpdatingProductDto updatingProductDto);

        /// <summary>
        /// Удалить товар.
        /// </summary>
        /// <param name="id"> Идентификатор товара. </param>
        public Task<bool> TryDeleteAsync(Guid id);
    }
}
