using AutoMapper;
using DeliveryService.Delivery.BusinessLogic.Models;
using DeliveryService.Delivery.BusinessLogic.Services.Contracts.Dto;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Delivery.Core.Models.Requests;
using DeliveryService.Delivery.Core.Models.Responses;
using DeliveryService.Delivery.Domain.Entities.DeliveryEntities;
using DeliveryService.Domain.External.Entities;

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
            .ForMember(dest => dest.DeliveryStatus, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.OrderStatus, opt => opt.Ignore())
            .ForMember(dest => dest.EstimatedDeliveryTime, opt => opt.Ignore())
            .ForMember(dest => dest.ActualDeliveryTime, opt => opt.Ignore());

            CreateMap<Domain.Entities.DeliveryEntities.Delivery, DeliveryResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserGuid, opt => opt.MapFrom(src => src.UserGuid))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
            .ForMember(dest => dest.DeliveryStatus, opt => opt.MapFrom(src => src.DeliveryStatus))
            .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.TotalQuantity))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress));

            CreateMap<DeliveryModel, DeliveryDto>();
            CreateMap<CreateDeliveryRequest, CreateDeliveryDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.DeliveryStatus, opt => opt.Ignore());
            CreateMap<Domain.Entities.DeliveryEntities.Delivery, CreateDeliveryResponse>();
        }
    }
}
