using AutoMapper;
using SharedLibrary.DeliveryService.Dto;
using SharedLibrary.DeliveryService.Models;

namespace WebHost.Mappers
{
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile()
        {
            CreateMap<Delivery, DeliveryResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserGuid, opt => opt.MapFrom(src => src.UserGuid))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))                
            .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.TotalQuantity))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress));

            CreateMap<CreateDeliveryDto, Delivery>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserGuid, opt => opt.MapFrom(src => src.UserGuid))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
            .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.TotalQuantity))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress));

            CreateMap<UpdateDeliveryDto, Delivery>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserGuid, opt => opt.MapFrom(src => src.UserGuid))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
            .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.TotalQuantity))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress));
        }
    }
}
