using DeliveryService.Delivery.BusinessLogic.Enums;

namespace DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories
{
    public interface IDeliveryRepository
    {
        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <returns> Cущность. </returns>
        DataAccess.Domain.Domain.Entities.Delivery? Get(Guid id);

        ///// <summary>
        ///// Получить сущность по Id.
        ///// </summary>
        ///// <param name="id"> Id сущности. </param>
        ///// <returns> Cущность. </returns>
        //Delivery GetUserId(Guid UserId);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность. </returns>
        Task<DataAccess.Domain.Domain.Entities.Delivery> GetAsync(Guid id, CancellationToken cancellationToken);

        ///// <summary>
        ///// Запросить все сущности в базе.
        ///// </summary>
        ///// <param name="cancellationToken"> Токен отмены. </param>
        ///// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
        ///// <returns> Список сущностей. </returns>
        Task<List<DataAccess.Domain.Domain.Entities.Delivery>> GetAllAsync(bool asNoTracking, CancellationToken cancellationToken);

        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="entity"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        Task<DataAccess.Domain.Domain.Entities.Delivery> AddAsync(DataAccess.Domain.Domain.Entities.Delivery entity);

        /// <summary>
        /// Для сущности определить состояние - то что она изменена.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        void Update(DataAccess.Domain.Domain.Entities.Delivery entity);
        /// <summary>
        /// Изменение сущности.
        /// </summary>
        /// <param name="entity"> Сущность для изменения. </param>
        Task<OperationResult> UpdateAsync(DataAccess.Domain.Domain.Entities.Delivery entity, bool isUpdateDeliveryStatus);
        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="id"> Id удалённой сущности. </param>
        /// <returns> Была ли сущность удалена. </returns>
        bool Delete(Guid id);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity"> Cущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        bool Delete(DataAccess.Domain.Domain.Entities.Delivery entity);
        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity"> Cущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        bool DeleteAsync(DataAccess.Domain.Domain.Entities.Delivery entity, CancellationToken cancellationToken);
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
