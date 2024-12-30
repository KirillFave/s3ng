using DeliveryService.Repositories;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.DTO;
using DeliveryService.Abstractions;

namespace DeliveryService.Repositories;

public interface IDeliveryRepository //: IRepository<Delivery, Guid>
{
    /// <summary>
    /// Создать доставку.
    /// </summary>
    /// <param name="createDeliveryDTO"> ДТО создаваемой перевозки. </param>
    public Task<CreateDeliveryDTO> AddAsync(CreateDeliveryDTO createDeliveryDTO);

    ///// <summary>
    ///// Получить доставку по id.
    ///// </summary>
    ///// <param name="id"> Идентификатор доставки. </param>
    ///// <returns> ДТО товара.</returns>
    //public Task<Guid> GetByIdAsync(Guid id);

    ///// <summary>
    ///// Изменить доставку по id.
    ///// </summary>
    ///// <param name="id"> Идентификатор доставки. </param>
    ///// <param name="updateDeliveryDTO"> ДТО редактируемого товара. </param>
    //public Task<bool> TryUpdateAsync(Guid id, UpdateDeliveryDTO updateProductDTO);

    ///// <summary>
    ///// Удалить доставку.
    ///// </summary>
    ///// <param name="id"> Идентификатор доставки. </param>
    //public Task<bool> TryDeleteAsync(Guid id);    
}
