using AutoMapper;
using DeliveryService.Delivery.BusinessLogic.Models;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;

namespace DeliveryService.Delivery.BusinessLogic.Services.Delivery.Implementations.Mapping
{ 
 /// <summary>
 /// Automapper Profile for Delivery Entities 
 /// </summary>
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile()
        {
            CreateMap<DeliveryModel, DeliveryDto>();
            CreateMap<CreateDeliveryModel, CreateDeliveryDto>();
            //CreateMap<DataAccess.Domain.Domain.Entities.Delivery, CreateDeliveryDto>();
            CreateMap<UpdateDeliveryModel, UpdateDeliveryDto>();
        }
    }
}

