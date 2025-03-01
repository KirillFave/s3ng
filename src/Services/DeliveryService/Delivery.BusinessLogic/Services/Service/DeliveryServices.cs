using AutoMapper;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Abstractions;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;

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
        /// Создать доставку.
        /// </summary>
        /// <param name="createDeliveryDto"> Дто создаваемой доставки. </param>
        public async Task<DataAccess.Domain.Domain.Entities.Delivery> CreateAsync(CreateDeliveryDto createDeliveryDto)
        {
            var delivery = _mapper.Map<CreateDeliveryDto, DataAccess.Domain.Domain.Entities.Delivery>(createDeliveryDto);
            var createdDelivery = await _deliveryRepository.AddAsync(delivery);
            await _deliveryRepository.SaveChangesAsync();
            return createdDelivery;
        }

        /// <summary>
        /// Получить доставку по id.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        /// <returns> DTO доставки.</returns>
        public async Task<DeliveryDto> GetByIdAsync(Guid id)
        {
            var delivery = await _deliveryRepository.GetAsync(id, CancellationToken.None);
            var deliveryDto = _mapper.Map<DeliveryDto>(delivery);

            return deliveryDto;
        }

        /// <summary>
        /// Изменить доставку по id.
        /// </summary>
        /// <param name="id"> Иентификатор доставки. </param>
        /// <param name="updateDeliveryDto"> ДТО редактируемого товара. </param>
        public async Task<bool> TryUpdateAsync(Guid id, UpdateDeliveryDto updateDeliveryDto)
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

            _deliveryRepository.Update(delivery);
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

            _deliveryRepository.Update(product);
            await _deliveryRepository.SaveChangesAsync();

            return true;
        }
    }
}
