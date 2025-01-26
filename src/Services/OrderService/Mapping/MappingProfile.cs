using OrderService.Dto;
using SharedLibrary.OrderService.Models;

using AutoMapper;
namespace OrderService.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, CreateOrderDto>();
    }
}
