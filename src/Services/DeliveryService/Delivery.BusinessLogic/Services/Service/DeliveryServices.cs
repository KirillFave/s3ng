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
        /// <param name="createDeliveryDTO"> ДТО создаваемой доставки. </param>
        public async Task<Guid> CreateAsync(CreateDeliveryDto createDeliveryDTO)
        {
            var delivery = _mapper.Map<CreateDeliveryDto, DataAccess.Domain.Domain.Entities.Delivery>(createDeliveryDTO);
            var createdDelivery = await _deliveryRepository.AddAsync(delivery);
            await _deliveryRepository.SaveChangesAsync();
            return createdDelivery.Id;
        }

        /// <summary>
        /// Получить доставку по id.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        /// <returns> DTO доставки.</returns>
        public async Task<DeliveryDto> GetByIdAsync(Guid id)
        {
            var delivery = await _deliveryRepository.GetAsync(id, CancellationToken.None);
            var deliveryDTO = _mapper.Map<DeliveryDto>(delivery);

            return deliveryDTO;
        }

        /// <summary>
        /// Изменить доставку по id.
        /// </summary>
        /// <param name="id"> Иентификатор доставки. </param>
        /// <param name="updateDeliveryDTO"> ДТО редактируемого товара. </param>
        public async Task<bool> TryUpdateAsync(Guid id, UpdateDeliveryDto updateDeliveryDTO)
        {
            var delivery = await _deliveryRepository.GetAsync(id, CancellationToken.None);

            if (delivery is null)
            {
                return false;
            }

            delivery.PaymentType = updateDeliveryDTO.PaymentType;
            delivery.DeliveryStatus = updateDeliveryDTO.DeliveryStatus;
            delivery.OrderId = updateDeliveryDTO.OrderId;
            delivery.CourierId = updateDeliveryDTO.CourierId;
            delivery.ShippingAddress = updateDeliveryDTO.ShippingAddress;
            delivery.TotalQuantity = updateDeliveryDTO.TotalQuantity;
            delivery.TotalPrice = updateDeliveryDTO.TotalPrice;
            delivery.EstimatedDeliveryTime = updateDeliveryDTO.EstimatedDeliveryTime;

            _deliveryRepository.Update(delivery);
            await _deliveryRepository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Удалить доставку.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        public async Task<bool> TryDeleteAsync(Guid id)
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
