using SharedLibrary.ProductService.Dto;

namespace ProductService.Domain
{
    /// <summary>
    /// Сервис управления продуктами
    /// </summary>
    public interface IProductManagementService
    {
        /// <summary>
        /// Получить все продукты
        /// </summary>
        Task<List<Product>> GetAllAsync();
        /// <summary>
        /// Получить продукт по ИДу
        /// </summary>
        Task<Product?> GetByIdAsync(string id);
        /// <summary>
        /// Создать продукт
        /// </summary>
        /// <returns>ИД</returns>
        Task<string> CreateAsync(ProductCreateDto dto);
        /// <summary>
        /// Обновить продукт
        /// </summary>
        /// <returns>ИД</returns>
        Task<string> UpdateAsync(ProductUpdateDto dto);
        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <param name="id">ИД</param>
        Task DeleteAsync(string id);
    }
}
