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
        Task<List<Product>> GetAllAsync(CancellationToken ct = default);
        /// <summary>
        /// Получить продукт по ИДу
        /// </summary>
        Task<Product?> GetByIdAsync(string id, CancellationToken ct = default);
        /// <summary>
        /// Создать продукт
        /// </summary>
        /// <returns>ИД</returns>
        Task CreateAsync(Product product, CancellationToken ct = default);
        /// <summary>
        /// Обновить продукт
        /// </summary>
        /// <returns>ИД</returns>
        Task UpdateAsync(Product product, CancellationToken ct = default);
        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <param name="id">ИД</param>
        Task DeleteAsync(string id, CancellationToken ct = default);
    }
}
