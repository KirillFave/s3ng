using System;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;


namespace DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions
{
    public interface IDeliveryService
    {
        /// <summary>
        /// Создать доставку.
        /// </summary>
        /// <param name="createDeliveryDto"> DTO создаваемой доставки. </param>
        public Task<Guid> CreateAsync(CreateDeliveryDto createDeliveryDto);

        /// <summary>   
        /// Получить доставку по id.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        /// <returns> DTO доставки.</returns>
        public Task<DeliveryDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Изменить доставку по id.
        /// </summary>
        /// <param name="id"> Иентификатор доставки. </param>
        /// <param name="updateDeliveryDto"> DTO редактируемой доставки. </param>
        public Task<bool> TryUpdateAsync(Guid id, UpdateDeliveryDto updateDeliveryDto);

        /// <summary>
        /// Удалить доставку.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        public Task<bool> TryDeleteAsync(Guid id);
    }
}
