using DeliveryService.Delivery.BusinessLogic.Enums;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Delivery.DataAccess.Data;
using System.Threading;
using DeliveryService.Delivery.Domain.Entities.DeliveryEntities;
using DeliveryService.Domain.External.Entities;

namespace DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        protected readonly DeliveryDBContext _context;
        private readonly DbSet<Domain.Entities.DeliveryEntities.Delivery> _entityDeliverySet;

        public DeliveryRepository(DeliveryDBContext context)
        {
            _context = context;
            _entityDeliverySet = _context.Set<Domain.Entities.DeliveryEntities.Delivery>();
        }
            /// <summary>
            /// Получить сущность по Id.
            /// </summary>
            /// <param name="id"> Id сущности. </param>        
            /// <returns> Cущность. </returns>
        public Domain.Entities.DeliveryEntities.Delivery? Get(Guid id, CancellationToken cancellationToken)
        {
            return _entityDeliverySet.Find(id);
        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность. </returns>
        public async Task<Domain.Entities.DeliveryEntities.Delivery> GetAsync(Guid id, CancellationToken cancellationToken = default)
            => await _entityDeliverySet.FindAsync(id, cancellationToken);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="userId"> Id сущности. </param>        
        /// <returns> Cущность. </returns>
        public Domain.Entities.DeliveryEntities.Delivery GetUserId(Guid userId, CancellationToken cancellationToken)
        {
            return _entityDeliverySet.Find(userId);
        }
        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="orderId"> Id сущности. </param>        
        /// <returns> Cущность. </returns>
        public async Task<Domain.Entities.DeliveryEntities.Delivery?> GetDeliveryByOrderIdAsync(Guid orderId, CancellationToken cancellationToken) =>
            //return _entityDeliverySet.Find(orderId);
            await _entityDeliverySet.FindAsync(orderId);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность. </returns>
        public async Task<Domain.Entities.DeliveryEntities.Delivery> GetAsync(Guid id, CancellationToken cancellationToken = default, string v = null)
            => await _entityDeliverySet.FindAsync(id, cancellationToken);

        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Список сущностей. </returns>       

        public async Task<List<Domain.Entities.DeliveryEntities.Delivery>> GetAllAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var entities = asNoTracking ? _entityDeliverySet.AsNoTracking() : _entityDeliverySet;
            return await entities.ToListAsync(cancellationToken);
        }
        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="delivery"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        public async Task<Domain.Entities.DeliveryEntities.Delivery> AddAsync(Domain.Entities.DeliveryEntities.Delivery delivery, CancellationToken cancellationToken)
        {
            await _context.Deliveries.AddAsync(delivery);   
            
            await _context.SaveChangesAsync(cancellationToken);
            return delivery;
        }
            /// <summary>
            /// Обновить в базе сущность.
            /// </summary>
            /// <param name="entity"> Сущность для обновления. </param>
            /// <returns> Обновленная сущность. </returns>
        public void Update(Domain.Entities.DeliveryEntities.Delivery delivery, CancellationToken cancellationToken)
        {
            delivery.LastUpdated = DateTime.Now;
            _entityDeliverySet.Entry(delivery).State = EntityState.Modified;
        }
            /// <summary>
            /// Обновить в базе сущность.
            /// </summary>
            /// <param name="entity"> Сущность для обновления. </param>
            /// <returns> Обновленная сущность. </returns>
        public async Task<OperationResult> UpdateAsync(Domain.Entities.DeliveryEntities.Delivery delivery, CancellationToken cancellationToken)
        {
            Domain.Entities.DeliveryEntities.Delivery? deliveryToUpdate = await _context.Deliveries.FindAsync(delivery.Id);
            if (deliveryToUpdate is null)
            {
                return OperationResult.NotEntityFound;
            }

            if (
                  deliveryToUpdate.PaymentType == delivery.PaymentType &&
                  deliveryToUpdate.DeliveryStatus == delivery.DeliveryStatus &&                                              
                  deliveryToUpdate.ShippingAddress == delivery.ShippingAddress &&
                  deliveryToUpdate.TotalQuantity == delivery.TotalQuantity &&                   
                  deliveryToUpdate.TotalPrice == delivery.TotalPrice &&                         
                  deliveryToUpdate.EstimatedDeliveryTime == delivery.EstimatedDeliveryTime    
               )                

            if (delivery.ShippingAddress is not null)
            {
                deliveryToUpdate.ShippingAddress = delivery.ShippingAddress;
            }
            //if (delivery.Items is not null)
            //{
            //    deliveryToUpdate.Items = delivery.Items;
            //}

            int stateEntriesWritten = await _context.SaveChangesAsync();
            return stateEntriesWritten > 0 ? OperationResult.Success : OperationResult.NotChangesApplied;
        }
            /// <summary>
            /// Удалить сущность.
            /// </summary>
            /// <param name="entity"> Cущность для удаления. </param>
            /// <returns> Была ли сущность удалена. </returns>
        public bool Delete(Domain.Entities.DeliveryEntities.Delivery delivery)
        {
            if (delivery is null)
            {
                return false;
            }
            _entityDeliverySet.Remove(delivery);
            _entityDeliverySet.Entry(delivery).State = EntityState.Deleted;

            return true;
        }           
            /// <summary>
            /// Удалить сущность.
            /// </summary>
            /// <param name="delivery"> Id удалённой сущности. </param>
            /// <returns> Была ли сущность удалена. </returns> public bool Delete(Product product)
        public bool DeleteAsync(Domain.Entities.DeliveryEntities.Delivery delivery, CancellationToken cancellationToken)
        {
            if (delivery is null)
            {
                return false;
            }
            _entityDeliverySet.Remove(delivery);
            _entityDeliverySet.Entry(delivery).State = EntityState.Deleted;

            return true;
        }

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="orderId"> Id сущности. </param>        
        /// <returns> Cущность. </returns>
        public async Task<Domain.Entities.DeliveryEntities.Delivery?> GetDeliveryByOrderIdAsync(Guid orderId)
        {
            try
            {
                var result = await _context.Deliveries.Where(p => p.OrderId == orderId).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }        

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
            /// <summary>
            /// Сохранить изменения.
            /// </summary>
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
       
    }
}
