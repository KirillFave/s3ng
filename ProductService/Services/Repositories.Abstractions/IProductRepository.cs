using s3ng.ProductService.Core.Domain.Managment;

namespace s3ng.ProductService.Services.Repositories.Abstractions
{
    public interface IProductRepository
    {
        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="noTracking"> Вызвать с AsNoTracking.</param>
        /// <returns> IQueryable массив сущностей.</returns>
        IQueryable<Product> GetAll(bool noTracking = false);

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены. </param>
        /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        /// <returns> Список сущностей. </returns>
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Cущность. </returns>
        Product Get(Guid id);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность. </returns>
        Task<Product> GetAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="id"> Id удалённой сущности. </param>
        /// <returns> Была ли сущность удалена. </returns>
        bool Delete(Guid id);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity"> Cущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        bool Delete(Product entity);

        /// <summary>
        /// Для сущности проставить состояние - что она изменена.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        void Update(Product entity);

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        Product Add(Product entity);

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        Task<Product> AddAsync(Product entity);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
