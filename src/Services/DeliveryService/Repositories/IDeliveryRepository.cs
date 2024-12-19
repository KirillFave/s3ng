using DeliveryService.Repositories;
using DeliveryService.Domain.Domain.Entities;

namespace DeliveryService.Repositories;

    public interface IDeliveryRepository<T>
    where T : 
{
    /// <summary>
    /// Запросить все сущности в базе.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены. </param>
    /// <returns> Список сущностей. </returns>
    Task<List<Delivery>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"></param>
    /// <returns> Cущность. </returns>
    Task<Delivery> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получить сущность по AuthenticationId.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"></param>
    /// <returns> Cущность. </returns>
    Task<Delivery> GetByAuthenticationIdAsync(Guid authenticationId, CancellationToken cancellationToken);

    /// <summary>
    /// Добавить в базу одну сущность.
    /// </summary>
    /// <param name="entity"> Сущность для добавления. </param>
    /// <returns> Добавленная сущность. </returns>
    Task<Delivery> AddAsync(T entity);

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="id"> Id удалённой сущности. </param>
    /// <returns> Была ли сущность удалена. </returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить сущность.
    /// </summary>
    /// <param name="entity"> Сущность для обновления. </param>
    /// <returns> Обновленная сущность. </returns>
    Task<Delivery> UpdateAsync(Delivery entity, CancellationToken cancellationToken);

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<Domain.Domain.Entities.Delivery> GetByIdAsync(Guid guid);
    Task<bool> AddAsync(Domain.Domain.Entities.Delivery delivery);
    Task<OperationResults> UpdateAsync(Domain.Domain.Entities.Delivery delivery);
    Task<OperationResults> DeleteAsync(Guid guid);
}


