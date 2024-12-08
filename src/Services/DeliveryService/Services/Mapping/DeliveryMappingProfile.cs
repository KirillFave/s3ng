using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using DeliveryService.Abstractions;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.DTO;

namespace DeliveryService.Services.Mapping
{
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile() 
        {
            CreateMap<Delivery, CreateDeliveryDTO>();
        }
    }
}
