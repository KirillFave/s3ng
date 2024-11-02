using OrderService.Dto;
using OrderService.Models;

using AutoMapper;
namespace OrderService.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, CreateOrderDto>();
    }
}
