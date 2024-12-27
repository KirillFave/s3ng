using Microsoft.EntityFrameworkCore;
using DeliveryService.Data;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Enums;
using DeliveryService.Domain.External.Entities;
using DeliveryService.Repositories;
using DeliveryService.DTO;

namespace DeliveryService.Repositories 
{
    /// <summary>
    /// Репозиторий.
    /// </summary>
    /// <typeparam name="T"> Тип сущности. </typeparam>
    /// <typeparam name="TPrimaryKey"> Тип первичного ключа. </typeparam>
    public class DeliveryRepository : Repository<Delivery, Guid>, IDeliveryRepository
    {
        public DeliveryRepository(DeliveryDBContext context): base(context) { }

        public Task<Guid> CreateAsync(CreateDeliveryDTO createDeliveryDTO) => throw new NotImplementedException();

        public Task<bool> TryDeleteAsync(Guid id) => throw new NotImplementedException();
        public Task<bool> TryUpdateAsync(Guid id, UpdateDeliveryDTO updateProductDTO) => throw new NotImplementedException();
        Task<Guid> IDeliveryRepository.GetByIdAsync(Guid id) => throw new NotImplementedException();
    }
}

////protected readonly DeliveryDBContext Context;
////private readonly DbSet<Delivery> ?_entityDeliverySet;

////public DeliveryRepository(DeliveryDBContext context)
////{
////    Context = context;
////    _entityDeliverySet = Context.Set<Delivery>();
////}

//#region Get     

///// <summary>
///// Получить сущность по Id.
///// </summary>
///// <param name="id"> Id сущности. </param>
///// <param name="cancellationToken"></param>
///// <returns> Cущность. </returns>
//public async Task<Delivery> GetByIdAsync(Guid id, CancellationToken cancellationToken)
//        {
//            return await _entityDeliverySet.FindAsync(id, cancellationToken);
//        }

//        /// <summary>
//        /// Получить сущность по UserId.
//        /// </summary>
//        /// <param name="userId"> Id сущности. </param>
//        /// <param name="cancellationToken"></param>
//        /// <returns> Cущность. </returns>
//        public async Task<Delivery> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
//        {
//            return await _entityDeliverySet.FindAsync(userId, cancellationToken);
//        }

//        #endregion

//        #region GetAll

//        /// <summary>
//        /// Запросить все сущности в базе.
//        /// </summary>
//        /// <param name="cancellationToken"> Токен отмены </param>
//        /// <returns> Список сущностей. </returns>
//        public async Task<List<Delivery>> GetAllAsync(CancellationToken cancellationToken)
//        {
//            return await _entityDeliverySet.ToListAsync(cancellationToken);
//        }

//        #endregion

//        #region Create 
        
//        /// <summary>
//        /// Добавить в базу одну сущность.
//        /// </summary>
//        /// <param name="entity"> Сущность для добавления. </param>
//        /// <returns> Добавленная сущность. </returns>
//        public async Task<Delivery> AddAsync(Delivery entity, CancellationToken cancellationToken)
//        {
//            return (await _entityDeliverySet.AddAsync(entity, cancellationToken)).Entity;
//        }

//        /// <summary>
//        /// Добавить в базу массив сущностей.
//        /// </summary>
//        /// <param name="entities"> Массив сущностей. </param>
//        public virtual async Task AddRangeAsync(ICollection<Delivery> entities)
//        {
//            if (entities == null || !entities.Any())
//            {
//                return;
//            }
//            await _entityDeliverySet.AddRangeAsync(entities);
//        }

//        #endregion

//        #region Update

//        /// <summary>
//        /// Для сущности определить состояние - на кое она изменена.
//        /// </summary>
//        /// <param name="entity"> Сущность для изменения. </param>
//        public virtual void Update(Delivery entity)
//        {
//            Context.Entry(entity).State = EntityState.Modified;
//        }

//        /// <summary>
//        /// Обновить в базе сущность.
//        /// </summary>
//        /// <param name="entity"> Сущность для обновления. </param>
//        /// <returns> Обновленная сущность. </returns>

//        //public async Task<Delivery> UpdateAsync(Delivery entity, CancellationToken cancellationToken)
//        //{
//        //    _entityDeliverySet.Update(entity);
//        //    return await _entityDeliverySet.FindAsync(entity.Id, cancellationToken);
//        //}
//        //public async Task<OperationResult> UpdateAsync(Delivery delivery, bool isUpdateDeliveryStatus)
//        //{
//        //    Delivery? deliveryToUpdate = await Context.Deliveries.FindAsync(delivery.Id);
//        //    if (deliveryToUpdate is null)
//        //    {
//        //        return OperationResult.NotEntityFound;
//        //    }

//        //    if (deliveryToUpdate.PaymentType == delivery.PaymentType &&
//        //        deliveryToUpdate.DeliveryStatus == delivery.DeliveryStatus &&
//        //        deliveryToUpdate.Items == delivery.Items &&
//        //        deliveryToUpdate.CourierId == delivery.CourierId &&
//        //        deliveryToUpdate.Shipping_Address == delivery.Shipping_Address &&
//        //        deliveryToUpdate.Total_Quantity == delivery.Total_Quantity &&
//        //        deliveryToUpdate.Total_Price == delivery.Total_Price &&
//        //        deliveryToUpdate.Estimated_Delivery_Time == delivery.Estimated_Delivery_Time                
//        //        )
//        //    {
//        //        return OperationResult.NotModified;
//        //    }

//        //    if (isUpdateDeliveryStatus)
//        //    {
//        //        deliveryToUpdate.PaymentType = delivery.PaymentType;
//        //    }

//        //    if (delivery.Shipping_Address is not null)
//        //    {
//        //        deliveryToUpdate.Shipping_Address = delivery.Shipping_Address;
//        //    }

//        //    int stateEntriesWritten = await Context.SaveChangesAsync();
//        //    return stateEntriesWritten > 0 ? OperationResult.Success : OperationResult.NotChangesApplied;
//        //}
//        public async Task<Delivery> UpdateAsync(Delivery entity, CancellationToken cancellationToken)
//        {
//            _entityDeliverySet.Update(entity);
//            return await _entityDeliverySet.FindAsync(entity.Id, cancellationToken);
//        }

//        #endregion

//        #region Delete

//        /// <summary>
//        /// Удалить сущность.
//        /// </summary>
//        /// <param name="id"> Id удаляемой сущности. </param>
//        /// <returns> Была ли сущность удалена. </returns>
//        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
//        {
//            var deleteEntity = await _entityDeliverySet.FindAsync(id, cancellationToken);
//            if (deleteEntity == null)
//            {
//                return false;
//            }

//            _entityDeliverySet.Remove(deleteEntity);
//            int stateEntriesWritten = await Context.SaveChangesAsync();
//            return true;
//        }

//        /// <summary>
//        /// Удалить сущность.
//        /// </summary>
//        /// <param name="entity"> Сущность для удаления. </param>
//        /// <returns> Была ли сущность удалена. </returns>
//        public virtual bool Delete(Delivery entity)
//        {
//            if (entity == null)
//            {
//                return false;
//            }
//            Context.Entry(entity).State = EntityState.Deleted;
//            return true;
//        }

//        /// <summary>
//        /// Удалить сущности.
//        /// </summary>
//        /// <param name="entities"> Коллекция сущностей для удаления. </param>
//        /// <returns> Была ли операция завершена успешно. </returns>
//        public virtual bool DeleteRange(ICollection<Delivery> entities)
//        {
//            if (entities == null || !entities.Any())
//            {
//                return false;
//            }
//            _entityDeliverySet.RemoveRange(entities);
//            return true;
//        }

//        #endregion

//        #region SaveChanges

//        /// <summary>
//        /// Сохранить изменения.
//        /// </summary>
//        /// 
//        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
//        {
//            await Context.SaveChangesAsync(cancellationToken);
//        }         
      
//        #endregion
//    }
//}
