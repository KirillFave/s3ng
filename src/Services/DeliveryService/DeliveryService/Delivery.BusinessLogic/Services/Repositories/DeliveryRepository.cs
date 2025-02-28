using DeliveryService.Delivery.BusinessLogic.Enums;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Delivery.DataAccess.Data;
using DeliveryService.Delivery.DataAccess.Domain.Domain.Entities;
using DeliveryService.Delivery.DataAccess.Domain.External.Entities;
//using DeliveryService.Kafka.Models;

namespace DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        protected readonly DeliveryDBContext _context;
        private readonly DbSet<DataAccess.Domain.Domain.Entities.Delivery> _entityDeliverySet;

        public DeliveryRepository(DeliveryDBContext context)
        {
            _context = context;
            _entityDeliverySet = _context.Set<DataAccess.Domain.Domain.Entities.Delivery>();
        }
            /// <summary>
            /// Получить сущность по Id.
            /// </summary>
            /// <param name="id"> Id сущности. </param>        
            /// <returns> Cущность. </returns>
        public DataAccess.Domain.Domain.Entities.Delivery? Get(Guid id)
        {
            return _entityDeliverySet.Find(id);
        }
            /// <summary>
            /// Получить сущность по Id.
            /// </summary>
            /// <param name="userId"> Id сущности. </param>        
            /// <returns> Cущность. </returns>
        public DataAccess.Domain.Domain.Entities.Delivery GetUserId(Guid userId)
        {
            return _entityDeliverySet.Find(userId);
        }
        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="orderId"> Id сущности. </param>        
        /// <returns> Cущность. </returns>
        public async Task<DataAccess.Domain.Domain.Entities.Delivery?> GetDeliveryByOrderIdAsync(Guid orderId, CancellationToken cancellationToken) =>
            //return _entityDeliverySet.Find(orderId);
            await _entityDeliverySet.FindAsync(orderId);

        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность. </returns>
        public async Task<DataAccess.Domain.Domain.Entities.Delivery> GetAsync(Guid id, CancellationToken cancellationToken = default)
            => await _entityDeliverySet.FindAsync(id, cancellationToken);


        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Список сущностей. </returns>       

        public async Task<List<DataAccess.Domain.Domain.Entities.Delivery>> GetAllAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var entities = asNoTracking ? _entityDeliverySet.AsNoTracking() : _entityDeliverySet;
            return await entities.ToListAsync(cancellationToken);
        }
        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="delivery"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        public async Task<DataAccess.Domain.Domain.Entities.Delivery> AddAsync(DataAccess.Domain.Domain.Entities.Delivery delivery)
        {
            await _context.Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();
            return delivery;
        }
            /// <summary>
            /// Обновить в базе сущность.
            /// </summary>
            /// <param name="entity"> Сущность для обновления. </param>
            /// <returns> Обновленная сущность. </returns>
        public void Update(DataAccess.Domain.Domain.Entities.Delivery delivery)
        {
            delivery.LastUpdated = DateTime.Now;
            _entityDeliverySet.Entry(delivery).State = EntityState.Modified;
        }
            /// <summary>
            /// Обновить в базе сущность.
            /// </summary>
            /// <param name="entity"> Сущность для обновления. </param>
            /// <returns> Обновленная сущность. </returns>
        public async Task<OperationResult> UpdateAsync(DataAccess.Domain.Domain.Entities.Delivery delivery, bool isUpdateDeliveryStatus)
        {
            DataAccess.Domain.Domain.Entities.Delivery? deliveryToUpdate = await _context.Deliveries.FindAsync(delivery.Id);
            if (deliveryToUpdate is null)
            {
                return OperationResult.NotEntityFound;
            }

            if (
                  deliveryToUpdate.PaymentType == delivery.PaymentType &&
                  deliveryToUpdate.DeliveryStatus == delivery.DeliveryStatus &&                                              
                  deliveryToUpdate.ShippingAddress == delivery.ShippingAddress &&
                  deliveryToUpdate.TotalQuantity == delivery.TotalQuantity &&                   // ?is required
                  deliveryToUpdate.TotalPrice == delivery.TotalPrice &&                         // ?is required
                  deliveryToUpdate.EstimatedDeliveryTime == delivery.EstimatedDeliveryTime    // ?is required
               )
                if (isUpdateDeliveryStatus)
                {
                    deliveryToUpdate.PaymentType = delivery.PaymentType;
                }

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
        public bool Delete(DataAccess.Domain.Domain.Entities.Delivery product)
        {
            if (product is null)
            {
                return false;
            }
            _entityDeliverySet.Remove(product);
            _entityDeliverySet.Entry(product).State = EntityState.Deleted;

            return true;
        }
            /// <summary>
            /// Удалить сущность.
            /// </summary>
            /// <param name="id"> Id удалённой сущности. </param>
            /// <returns> Была ли сущность удалена. </returns>
        public bool Delete(Guid id)
        {
            DataAccess.Domain.Domain.Entities.Delivery product = Get(id);

            return Delete(product);
        }
            /// <summary>
            /// Удалить сущность.
            /// </summary>
            /// <param name="delivery"> Id удалённой сущности. </param>
            /// <returns> Была ли сущность удалена. </returns> public bool Delete(Product product)
        public bool DeleteAsync(DataAccess.Domain.Domain.Entities.Delivery delivery, CancellationToken cancellationToken)
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
        public async Task<DataAccess.Domain.Domain.Entities.Delivery?> GetDeliveryByOrderIdAsync(Guid orderId)
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
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="order"> Id сущности. </param>        
        /// <returns> Cущность. </returns>
        public async Task SaveDeliveryStatus(Order order)
        {
            var delivery = await _context.Deliveries.FirstOrDefaultAsync(d => d.Id == order.Id);
            if (delivery == null)
            {
                delivery = new DataAccess.Domain.Domain.Entities.Delivery
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    DeliveryStatus = DeliveryStatus.AwaitingShipment,
                    LastUpdated = DateTime.UtcNow,
                    ShippingAddress = delivery.ShippingAddress,

                    History = new List<DeliveryHistory>()
                };

                var history = new DeliveryHistory
                {
                    Id = Guid.NewGuid(),
                    DeliveryId = delivery.Id,
                    DeliveryStatus = delivery.DeliveryStatus,
                    Timestamp = DateTime.UtcNow,
                    Comments = "Status updated based on Order status (Статус обновляется в зависимости от статуса заказа)."
                };
                delivery.History?.Add(history);

                _context.Deliveries.Add(delivery);
                await _context.SaveChangesAsync();
                return;
            }

            delivery.DeliveryStatus = GetDeliveryStatusFromOrderStatus(order.OrderStatus);

            delivery.LastUpdated = DateTime.UtcNow;

            var deliveryHistory = new DeliveryHistory
            {
                Id = Guid.NewGuid(),
                DeliveryId = delivery.Id,
                DeliveryStatus = delivery.DeliveryStatus,
                Timestamp = DateTime.Now,
                Comments = "Status updated based on Order status(Статус обновляется в зависимости от статуса заказа)."
            };
            delivery.History?.Add(deliveryHistory);

            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();
        }

        private static DeliveryStatus GetDeliveryStatusFromOrderStatus(OrderState orderStatus)
        {
            return orderStatus switch
            {
                OrderState.Pending => DeliveryStatus.AwaitingShipment,
                OrderState.Processing => DeliveryStatus.Shipped,
                OrderState.Delivering => DeliveryStatus.InTransit,
                OrderState.Completed => DeliveryStatus.Delivered,
                OrderState.Cancelled => DeliveryStatus.Cancelled,
                _ => DeliveryStatus.AwaitingShipment,
            };
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
