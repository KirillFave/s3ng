using DeliveryService.Services.Services.Contracts.DTO;

namespace DeliveryService.Services.Services.Abstractions
{
    public interface IDeliveryService
    {
        /// <summary>
        /// Создать доставку.
        /// </summary>
        /// <param name="createDeliveryDTO"> DTO создаваемой доставки. </param>
        public Task<Guid> CreateAsync(CreateDeliveryDTO createDeliveryDTO);

        /// <summary>   
        /// Получить доставку по id.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        /// <returns> DTO доставки.</returns>
        public Task<DeliveryDTO> GetByIdAsync(Guid id);

        /// <summary>
        /// Изменить доставку по id.
        /// </summary>
        /// <param name="id"> Иентификатор доставки. </param>
        /// <param name="updateDeliveryDTO"> DTO редактируемой доставки. </param>
        public Task<bool> TryUpdateAsync(Guid id, UpdateDeliveryDTO updateDeliveryDTO);

        /// <summary>
        /// Удалить доставку.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        public Task<bool> TryDeleteAsync(Guid id);
    }
}
