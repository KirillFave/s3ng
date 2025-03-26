using AutoMapper;
using DeliveryService.Delivery.BusinessLogic.Enums;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;
using DeliveryService.Delivery.Core.Models.Responses;
using DeliveryService.Delivery.DataAccess.Data;
using DeliveryService.Delivery.Domain.Entities.DeliveryEntities;
using DeliveryService.Delivery.Domain.Entities.External.Entities;
using DeliveryService.Domain.External.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace DeliveryService.Delivery.BusinessLogic.Services.DeliveryService
{
    public class DeliveryServices : IDeliveryService
    {
        private readonly DeliveryDBContext _context;
        //private readonly OrderContext _context;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IMapper _mapper;        

        public DeliveryServices(DeliveryDBContext context, IDeliveryRepository deliveryRepository, IMapper mapper)
        {
            _deliveryRepository = deliveryRepository;
            _mapper = mapper;
            _context = context;          
        }

        /// <summary>
        /// Получить доставку по id.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        /// <returns> Dto доставки </returns>
        public async Task<Domain.Entities.DeliveryEntities.Delivery> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryRepository.GetAsync(id, CancellationToken.None);

            return delivery;
        }

        public async Task<Domain.Entities.DeliveryEntities.Delivery> GetDeliveryByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryRepository.GetAsync(orderId, CancellationToken.None);

            return delivery;

        }

        /// <summary>
        /// Создать доставку.
        /// </summary>
        /// <param name="createDeliveryDto"> Дто создаваемой доставки. </param>
        public async Task<Domain.Entities.DeliveryEntities.Delivery> CreateAsync(CreateDeliveryDto createDeliveryDto, CancellationToken cancellationToken)
        {
            var delivery = _mapper.Map<CreateDeliveryDto, Domain.Entities.DeliveryEntities.Delivery>(createDeliveryDto);
            var createdDelivery = await _deliveryRepository.AddAsync(delivery, cancellationToken);
            await _deliveryRepository.SaveChangesAsync();
            return createdDelivery;
        }        

        /// <summary>
        /// Изменить доставку по id.
        /// </summary>
        /// <param name="id"> Иентификатор доставки. </param>
        /// <param name="updateDeliveryDto"> ДТО редактируемого товара. </param>
        public async Task<bool> UpdateAsync(Guid id, UpdateDeliveryDto updateDeliveryDto, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryRepository.GetAsync(id, CancellationToken.None);

            if (delivery is null)
            {
                return false;
            }

            delivery.PaymentType = updateDeliveryDto.PaymentType;           
            delivery.ShippingAddress = updateDeliveryDto.ShippingAddress;
            delivery.TotalQuantity = updateDeliveryDto.TotalQuantity;
            delivery.TotalPrice = updateDeliveryDto.TotalPrice;
            delivery.EstimatedDeliveryTime = updateDeliveryDto.EstimatedDeliveryTime;

            _ = _deliveryRepository.UpdateAsync(delivery, cancellationToken);
            await _deliveryRepository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Удалить доставку.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        public async Task<bool> TryDeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _deliveryRepository.GetAsync(id, CancellationToken.None);

            if (product is null)
            {
                return false;
            }

            product.IsDeleted = true;

            _deliveryRepository.DeleteAsync(product, cancellationToken);
            await _deliveryRepository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Сохранение статуса доставки.
        /// </summary>
        /// <param name="order"> Id сущности. </param>        
        /// <returns> Cущность. </returns>
        public async Task SaveDeliveryStatus(Order order)
        {
            var delivery = await _context.Deliveries.FirstOrDefaultAsync(d => d.Id == order.Id);
            if (delivery == null)
            {
                delivery = new Domain.Entities.DeliveryEntities.Delivery
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
                Comments = "Status updated based on Order status / Статус обновляется в зависимости от статуса заказа)."
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
    }
}
