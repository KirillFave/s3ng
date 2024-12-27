using DeliveryService.Repositories;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.DTO;

namespace DeliveryService.Repositories;

public interface IDeliveryRepository
{
    /// <summary>
    /// Создать доставку.
    /// </summary>
    /// <param name="createDeliveryDTO"> ДТО создаваемого товара. </param>
    public Task<Guid> CreateAsync(CreateDeliveryDTO createDeliveryDTO);

    /// <summary>
    /// Получить доставку по id.
    /// </summary>
    /// <param name="id"> Идентификатор доставки. </param>
    /// <returns> ДТО товара.</returns>
    public Task<Guid> GetByIdAsync(Guid id);

    /// <summary>
    /// Изменить доставку по id.
    /// </summary>
    /// <param name="id"> Идентификатор доставки. </param>
    /// <param name="updateDeliveryDTO"> ДТО редактируемого товара. </param>
    public Task<bool> TryUpdateAsync(Guid id, UpdateDeliveryDTO updateProductDTO);

    /// <summary>
    /// Удалить доставку.
    /// </summary>
    /// <param name="id"> Идентификатор товара. </param>
    public Task<bool> TryDeleteAsync(Guid id);   
}
