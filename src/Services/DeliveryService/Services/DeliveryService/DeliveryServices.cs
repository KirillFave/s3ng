using AutoMapper;
using DeliveryService.Services.Services.Repositories;
using DeliveryService.Services.Services.Abstractions;
using DeliveryService.Services.Services.Contracts.DTO;
using DeliveryService.Models;
using DeliveryService.Domain.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DeliveryService.Services.DeliveryService
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
        public async Task<Guid> CreateAsync(CreateDeliveryDTO createDeliveryDTO)
        {
            var delivery = _mapper.Map<Delivery>(createDeliveryDTO);
            var createDelivery = await _deliveryRepository.AddAsync(delivery);
            await _deliveryRepository.SaveChangesAsync();

            return createDelivery.Id;
        }      

        /// <summary>
        /// Получить доставку по id.
        /// </summary>
        /// <param name="id"> Идентификатор доставки. </param>
        /// <returns> DTO доставки.</returns>
        public async Task<DeliveryDTO> GetByIdAsync(Guid id)
        {
            var delivery = await _deliveryRepository.GetAsync(id, CancellationToken.None);
            var deliveryDTO = _mapper.Map<DeliveryDTO>(delivery);

            return deliveryDTO;
        }

        /// <summary>
        /// Изменить доставку по id.
        /// </summary>
        /// <param name="id"> Иентификатор доставки. </param>
        /// <param name="updateDeliveryDTO"> ДТО редактируемого товара. </param>
        public async Task<bool> TryUpdateAsync(Guid id, UpdateDeliveryDTO updateDeliveryDTO)
        {
            var delivery = await _deliveryRepository.GetAsync(id, CancellationToken.None);

            if (delivery is null)
            {
                return false;
            }

            delivery.PaymentType = updateDeliveryDTO.PaymentType;
            delivery.DeliveryStatus = updateDeliveryDTO.DeliveryStatus;
            //delivery.Items = updateDeliveryDTO.Items;
            //delivery.CourierId = updateDeliveryDTO.CourierId;
            delivery.ShippingAddress = updateDeliveryDTO.ShippingAddress;
            delivery.TotalQuantity = updateDeliveryDTO.TotalQuantity;
            delivery.TotalPrice = updateDeliveryDTO.TotalPrice;
            //delivery.UserId = updateDeliveryDTO.UserId;
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
