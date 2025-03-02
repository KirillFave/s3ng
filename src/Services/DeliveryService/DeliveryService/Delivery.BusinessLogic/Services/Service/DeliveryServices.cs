using AutoMapper;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;
using DeliveryService.Delivery.Core.Models.Responses;

namespace DeliveryService.Delivery.BusinessLogic.Services.DeliveryService
{
    public class DeliveryServices : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IMapper _mapper;

        public DeliveryServices(IDeliveryRepository deliveryRepository, IMapper mapper)
        {
            _deliveryRepository = deliveryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить доставку по id.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        /// <returns> DTO доставки.</returns>
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
            delivery.DeliveryStatus = updateDeliveryDto.DeliveryStatus;
            delivery.OrderId = updateDeliveryDto.OrderId;            
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
    }
}
