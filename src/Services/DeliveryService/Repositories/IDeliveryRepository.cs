using DeliveryService.Repositories;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.DTO;

namespace DeliveryService.Repositories;

    public interface IDeliveryRepository    
{
    /// <summary>
    /// Создать доставку.
    /// </summary>
    /// <param name="creatingProductDto"> ДТО создаваемого товара. </param>
    public Task<Guid> CreateAsync(CreateDeliveryDTO creatingDeliveryDTO);
    /// <summary>
    /// Запросить все сущности в базе.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены </param>
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
    /// Изменить товар по id.
    /// </summary>
    /// <param name="id"> Иентификатор товара. </param>
    /// <param name="updatingProductDto"> ДТО редактируемого товара. </param>
    public Task<bool> TryUpdateAsync(Guid id, UpdateDeliveryDTO updateDeliveryDTO);

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
    Task<Delivery> AddAsync(Delivery entity);

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
    Task<OperationResults> UpdateAsync(Domain.Domain.Entities.Delivery delivery);
    Task<OperationResults> DeleteAsync(Guid guid);
}


