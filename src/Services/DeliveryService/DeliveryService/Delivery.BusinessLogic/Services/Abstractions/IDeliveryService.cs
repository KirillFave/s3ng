using System;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;


namespace DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions
{
    public interface IDeliveryService
    {
        /// <summary>   
        /// Получить доставку по id.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        /// <returns> Dto доставки.</returns>
        Task<DeliveryDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Создать доставку.
        /// </summary>
        /// <param name="createDeliveryDto"> Dto создаваемой доставки. </param>
        //public Task<Guid> CreateAsync(CreateDeliveryDto createDeliveryDto);
        Task<DataAccess.Domain.Domain.Entities.Delivery> CreateAsync(CreateDeliveryDto createDeliveryDto);           

        /// <summary>
        /// Изменить доставку по id.
        /// </summary>
        /// <param name="id"> Иентификатор доставки. </param>
        /// <param name="updateDeliveryDto"> Dto редактируемой доставки. </param>
        Task<bool> TryUpdateAsync(Guid id, UpdateDeliveryDto updateDeliveryDto);

        /// <summary>
        /// Удалить доставку.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        Task<bool> TryDeleteAsync(Guid id);
    }
}
