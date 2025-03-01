using SharedLibrary.OrderService.Dto;
using SharedLibrary.OrderService.Models;

using AutoMapper;

namespace OrderService.Mapping;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, GetOrderResponseDto>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.UserGuid, opt => opt.MapFrom(src => src.UserGuid))
           .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
           .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
           .ForMember(dest => dest.ShipAddress, opt => opt.MapFrom(src => src.ShipAddress))
           .ForMember(dest => dest.CreatedTimestamp, opt => opt.MapFrom(src => src.CreatedTimestamp));

        CreateMap<CreateOrderDto, Order>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems))
            .ForMember(dest => dest.UserGuid, opt => opt.MapFrom(src => src.UserGuid))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
            .ForMember(dest => dest.ShipAddress, opt => opt.MapFrom(src => src.ShipAddress));

        CreateMap<UpdateOrderDto, Order>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
           .ForMember(dest => dest.ShipAddress, opt => opt.MapFrom(src => src.ShipAddress));
    }
}
