using SharedLibrary.OrderService.Dto;
using SharedLibrary.OrderService.Models;

using AutoMapper;

namespace OrderService.Mapping;

public class OrderItemMappingProfile : Profile
{
    public OrderItemMappingProfile()
    {
        CreateMap<OrderItem, GetOrderItemResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.ProductGuid, opt => opt.MapFrom(src => src.ProductGuid))
            .ForMember(dest => dest.PricePerUnit, opt => opt.MapFrom(src => src.PricePerUnit))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));

        CreateMap<CreateOrderItemDto, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.ProductGuid, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.PricePerUnit, opt => opt.MapFrom(src => src.PricePerUnit))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));
    }
}
