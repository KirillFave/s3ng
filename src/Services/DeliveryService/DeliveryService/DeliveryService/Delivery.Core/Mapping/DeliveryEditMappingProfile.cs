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
    public class DeliveryEditMappingProfile : Profile
    {
        public DeliveryEditMappingProfile()
        {
            CreateMap<EditDeliveryRequest, UpdateDeliveryDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserGuid, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore());
            CreateMap<UpdateDeliveryDto, Domain.Entities.DeliveryEntities.Delivery>();
            CreateMap<Domain.Entities.DeliveryEntities.Delivery, UpdateDeliveryDto>();
            CreateMap<EditDeliveryResponse, Domain.Entities.DeliveryEntities.Delivery>();
            CreateMap<Domain.Entities.DeliveryEntities.Delivery, EditDeliveryResponse>();

            CreateMap<EditDeliveryRequest, UpdateDeliveryDto>()
           .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<EditDeliveryRequest, EditDeliveryResponse>();
        }
    }
}

