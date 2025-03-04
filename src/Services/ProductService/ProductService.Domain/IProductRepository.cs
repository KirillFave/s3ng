namespace ProductService.Domain
{
    /// <summary>
    /// Репозиторий продуктов
    /// </summary>
    public interface IProductRepository
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
        Task CreateAsync(Product product);
        /// <summary>
        /// Обновить продукт
        /// </summary>
        /// <returns>ИД</returns>
        Task UpdateAsync(Product product);
        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <param name="id">ИД</param>
        Task DeleteAsync(string id);
    }
}
