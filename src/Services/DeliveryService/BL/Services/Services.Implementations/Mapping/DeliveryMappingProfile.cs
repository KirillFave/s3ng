using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using DeliveryService.Services.Services.Abstractions;
using DeliveryService.BL.Services.Services.Contracts.DTO;
using DeliveryService.DataAccess.Models;
using DeliveryService.DataAccess.Domain.Domain.Entities;

namespace DeliveryService.BL.Services.Services.Implementations.Mapping
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
