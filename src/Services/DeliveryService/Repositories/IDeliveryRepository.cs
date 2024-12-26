using DeliveryService.Repositories;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.DTO;
using DeliveryService.

namespace DeliveryService.Repositories;

public interface IDeliveryRepository    
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
    /// Обновить сущность.
    /// </summary>
    /// <param name="entity"> Сущность для обновления. </param>
    /// <returns> Обновленная сущность. </returns>
    public Task<bool> UpdateAsync(Delivery entity, CancellationToken cancellationToken);

    /// <summary>
    /// Получить сущность по UserId.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"></param>
    /// <returns> Cущность. </returns>
    Task<Domain.Domain.Entities.Delivery> GetByUserIdAsync(Guid UserGuid, CancellationToken cancellationToken);

    /// <summary>
    /// Добавить в базу одну сущность.
    /// </summary>
    /// <param name="entity"> Сущность для добавления. </param>
    /// <returns> Добавленная сущность. </returns>
    Task<Delivery> AddAsync(Delivery entity, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="id"> Id удалённой сущности. </param>
    /// <returns> Была ли сущность удалена. </returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);    
