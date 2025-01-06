using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using DeliveryService.Abstractions;
using DeliveryService.Domain.Domain.Entities;
using DeliveryService.DTO;
using DeliveryService.Models;

namespace DeliveryService.Services.Mapping
{
    /// <summary>
    /// Automapper Profile for Delivery Entities 
    /// </summary>
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile() 
        {
            CreateMap<DeliveryModel, DeliveryDTO>();
            CreateMap<CreateDeliveryModel, CreateDeliveryDTO>();
            CreateMap < UpdateDeliveryModel, UpdateDeliveryDTO>();
        }
    }
}
