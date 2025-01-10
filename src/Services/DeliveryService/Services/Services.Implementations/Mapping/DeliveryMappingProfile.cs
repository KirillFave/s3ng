using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using DeliveryService.Services.Services.Abstractions;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.Models;
using DeliveryService.Services.Services.Contracts.DTO;

namespace DeliveryService.Services.Services.Implementations.Mapping
{
    /// <summary>
    /// Automapper Profile for Delivery Entities 
    /// </summary>
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile()
        {
            CreateMap<DeliveryModel, DeliveryDTO>();
            //CreateMap<CreateDeliveryModel, CreateDeliveryDTO>();
            CreateMap<Delivery, CreateDeliveryDTO>();
            CreateMap<UpdateDeliveryModel, UpdateDeliveryDTO>();
        }
    }
}
