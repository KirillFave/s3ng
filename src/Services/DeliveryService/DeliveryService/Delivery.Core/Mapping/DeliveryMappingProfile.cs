using AutoMapper;
using DeliveryService.Delivery.BusinessLogic.Models;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Delivery.Core.Models.Requests;
using DeliveryService.Delivery.Core.Models.Responses;

namespace DeliveryService.Delivery.Core.Mapping
{
    /// <summary>
    /// Automapper Profile for Delivery Entities 
    /// </summary>
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile()
        {
            CreateMap<CreateDeliveryDto, Domain.Entities.DeliveryEntities.Delivery>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DeliveryStatus, opt => opt.MapFrom(src => src.DeliveryStatus))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
            .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.TotalQuantity))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
            .ForMember(dest => dest.EstimatedDeliveryTime, opt => opt.MapFrom(src => src.EstimatedDeliveryTime))
            .ForMember(dest => dest.ActualDeliveryTime, opt => opt.MapFrom(src => src.ActualDeliveryTime));

            CreateMap<Domain.Entities.DeliveryEntities.Delivery, CreateDeliveryResponse>();
            CreateMap<CreateDeliveryRequest, CreateDeliveryDto>();            
        }
    }
}
