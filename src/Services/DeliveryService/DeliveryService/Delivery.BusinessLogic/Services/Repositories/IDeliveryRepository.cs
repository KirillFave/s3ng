using DeliveryService.Delivery.BusinessLogic.Enums;
using DeliveryService.Domain.External.Entities;

namespace DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories
{
    public interface IDeliveryRepository
    {
        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Cущность. </returns>
        Domain.Entities.DeliveryEntities.Delivery? Get(Guid id, CancellationToken cancellationToken);

        ///// <summary>
        ///// Получить сущность по Id.
        ///// </summary>
        ///// <param name="id"> Id сущности. </param>
        ///// <returns> Cущность. </returns>
        Domain.Entities.DeliveryEntities.Delivery GetUserId(Guid UserId, CancellationToken cancellationToken);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность. </returns>
        Task<Domain.Entities.DeliveryEntities.Delivery> GetAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="orderId"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность. </returns>
        Task<Domain.Entities.DeliveryEntities.Delivery?> GetDeliveryByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);

        /// <summary>
        /// Сохранение статуса доставки.
        /// </summary>
        Task SaveDeliveryStatus(Order order);

        ///// <summary>
        ///// Запросить все сущности в базе.
        ///// </summary>
        ///// <param name="cancellationToken"> Токен отмены. </param>
        ///// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        ///// <returns> Список сущностей. </returns>
        Task<List<Domain.Entities.DeliveryEntities.Delivery>> GetAllAsync(bool asNoTracking, CancellationToken cancellationToken);

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>        
        /// <returns> Добавленная сущность. </returns>
        Task<Domain.Entities.DeliveryEntities.Delivery> AddAsync(Domain.Entities.DeliveryEntities.Delivery entity, CancellationToken cancellationToken);

        /// <summary>
        /// Для сущности определить состояние - то что она изменена.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        void Update(Domain.Entities.DeliveryEntities.Delivery entity, CancellationToken cancellationToken);
        /// <summary>
        /// Изменение сущности.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        Task<OperationResult> UpdateAsync(Domain.Entities.DeliveryEntities.Delivery entity, CancellationToken cancellationToken);
        
        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity"> Cущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        bool Delete(Domain.Entities.DeliveryEntities.Delivery entity);
        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="delivery"> Cущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        bool DeleteAsync(Domain.Entities.DeliveryEntities.Delivery delivery, CancellationToken cancellationToken);
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
