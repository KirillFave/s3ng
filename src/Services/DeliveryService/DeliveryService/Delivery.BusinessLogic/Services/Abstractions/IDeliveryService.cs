using System;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Domain.External.Entities;


namespace DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions
{
    public interface IDeliveryService
    {
        /// <summary>   
        /// Получить доставку по id.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        /// <returns> Dto доставки.</returns>
        Task<Domain.Entities.DeliveryEntities.Delivery> GetByIdAsync(Guid id, CancellationToken cancellationToken);


        /// <summary>   
        /// Получить доставку по id.
        /// </summary>
        /// <param name="orderId"> Идентификатор доставки. </param>
        /// <returns> Dto доставки.</returns>
        Task<Domain.Entities.DeliveryEntities.Delivery> GetDeliveryByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);

        /// <summary>
        /// Создать доставку.
        /// </summary>
        /// <param name="createDeliveryDto"> Dto создаваемой доставки. </param>
        //public Task<Guid> CreateAsync(CreateDeliveryDto createDeliveryDto);
        Task<Domain.Entities.DeliveryEntities.Delivery> CreateAsync(CreateDeliveryDto createDeliveryDto, CancellationToken cancellationToken);

        /// <summary>
        /// Сохранение статуса доставку.
        /// </summary>
        Task SaveDeliveryStatus(Order order);

        /// <summary>
        /// Изменить доставку по id.
        /// </summary>
        /// <param name="id"> Иентификатор доставки. </param>
        /// <param name="updateDeliveryDto"> Dto редактируемой доставки. </param>
        Task<bool> UpdateAsync(Guid id, UpdateDeliveryDto updateDeliveryDto, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить доставку.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        Task<bool> TryDeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
