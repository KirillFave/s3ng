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
            CreateMap<CreateDeliveryDto, DataAccess.Domain.Domain.Entities.Delivery>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DeliveryStatus, opt => opt.MapFrom(src => src.DeliveryStatus))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))            
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
            .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.TotalQuantity))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
            .ForMember(dest => dest.EstimatedDeliveryTime, opt => opt.MapFrom(src => src.EstimatedDeliveryTime));
            
            CreateMap<CreateDeliveryModel, CreateDeliveryDto>();
            CreateMap<DataAccess.Domain.Domain.Entities.Delivery, CreateDeliveryDto>();
            CreateMap<UpdateDeliveryModel, UpdateDeliveryDto>();
        }
    }
}

