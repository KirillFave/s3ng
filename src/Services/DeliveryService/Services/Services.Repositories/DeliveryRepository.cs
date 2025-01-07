using Microsoft.EntityFrameworkCore;
using DeliveryService.Data;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Enums;
using DeliveryService.Domain.External.Entities;
using DeliveryService.Services.Services.Contracts.DTO;
using AutoMapper;
using DeliveryService.Services.Services.Abstractions;
using DeliveryService.Models;

namespace DeliveryService.Services.Services.Repositories
{

    public class DeliveryRepository : IDeliveryRepository
    {
        protected readonly DeliveryDBContext _context;
        private readonly DbSet<Delivery>? _entityDeliverySet;

        public DeliveryRepository(DeliveryDBContext context)
        {
            _context = context;
            _entityDeliverySet = _context.Set<Delivery>();
        }
        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>        
        /// <returns> Cущность. </returns>
        public Delivery Get(Guid id)
        {
            return _entityDeliverySet.Find(id);
        }
        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>        
        /// <returns> Cущность. </returns>
        public Delivery GetUserId(Guid UserId)
        {
            return _entityDeliverySet.Find(UserId);
        }
        /// <summary>
        /// Получить сущность по Id.
        /// </summary>
        /// <param name="id"> Id сущности. </param>
        /// <param name="cancellationToken"></param>
        /// <returns> Cущность. </returns>
        public async Task<Delivery> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (_entityDeliverySet == null)
            {
                return await _entityDeliverySet.FindAsync(id);
            }
            else
            {
                return _entityDeliverySet.Find(id);
            }
        }
        /// <summary>
        /// Запросить все сущности в базе.
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Список сущностей. </returns>
        public IQueryable<Delivery> GetAll(bool asNoTracking = false)
        {
            return asNoTracking ? _entityDeliverySet.AsNoTracking() : _entityDeliverySet;
        }

        public async Task<List<Delivery>> GetAllAsync(CancellationToken cancellationToken = default, bool asNoTracking = false)
        {
            return await GetAll().ToListAsync(cancellationToken);
        }
        /// <summary>
        /// Добавить в базу одну сущность.
        /// </summary>
        /// <param name="delivery"> Сущность для добавления. </param>
        /// <returns> Добавленная сущность. </returns>
        public async Task<Delivery> AddAsync(Delivery entity)
        {
            await _context.Deliveries.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        /// <summary>
        /// Обновить в базе сущность.
        /// </summary>
        /// <param name="entity"> Сущность для обновления. </param>
        /// <returns> Обновленная сущность. </returns>
        public void Update(Delivery delivery)
        {
            delivery.TimeModified = DateTime.Now;
            _entityDeliverySet.Entry(delivery).State = EntityState.Modified;
        }
        /// <summary>
        /// Обновить в базе сущность.
        /// </summary>
        /// <param name="entity"> Сущность для обновления. </param>
        /// <returns> Обновленная сущность. </returns>
        public async Task<OperationResult> UpdateAsync(Delivery delivery, bool isUpdateDeliveryStatus)
        {
            Delivery? deliveryToUpdate = await _context.Deliveries.FindAsync(delivery.Id);
            if (deliveryToUpdate is null)
            {
                return OperationResult.NotEntityFound;
            }

            if (deliveryToUpdate.PaymentType == delivery.PaymentType &&
              deliveryToUpdate.DeliveryStatus == delivery.DeliveryStatus &&
              deliveryToUpdate.Items == delivery.Items &&
              deliveryToUpdate.CourierId == delivery.CourierId &&                             // ?is required
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
            if (delivery.Items is not null)
            {
                deliveryToUpdate.Items = delivery.Items;
            }

            int stateEntriesWritten = await _context.SaveChangesAsync();
            return stateEntriesWritten > 0 ? OperationResult.Success : OperationResult.NotChangesApplied;
        }
        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity"> Cущность для удаления. </param>
        /// <returns> Была ли сущность удалена. </returns>
        public bool Delete(Delivery product)
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
            Delivery product = Get(id);

            return Delete(product);
        }
        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="id"> Id удалённой сущности. </param>
        /// <returns> Была ли сущность удалена. </returns> public bool Delete(Product product)
        public bool DeleteAsync(Delivery delivery, CancellationToken cancellationToken)
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
