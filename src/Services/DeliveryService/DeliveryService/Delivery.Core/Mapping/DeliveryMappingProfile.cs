using AutoMapper;
using DeliveryService.Delivery.BusinessLogic.Models;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Contracts.Dto;
using DeliveryService.Delivery.Core.Models.Requests;
using DeliveryService.Delivery.Core.Models.Responses;
using DeliveryService.Delivery.DataAccess.Domain.Domain.Entities;

namespace DeliveryService.Delivery.Core.Mapping
{
    /// <summary>
    /// Automapper Profile for Delivery Entities 
    /// </summary>
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile()
        {
            CreateMap<CreateDeliveryDto, DataAccess.Domain.Domain.Entities.Delivery>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            //.ForMember(dest => dest.DeliveryStatus, opt => opt.MapFrom(src => src.DeliveryStatus))
            //.ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            //.ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus))
            //.ForMember(dest => dest.CourierId, opt => opt.MapFrom(src => src.CourierId))            
            //.ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.TotalQuantity))
            //.ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
            //.ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
            //.ForMember(dest => dest.EstimatedDeliveryTime, opt => opt.MapFrom(src => src.EstimatedDeliveryTime))
            //.ForMember(dest => dest.ActualDeliveryTime, opt => opt.MapFrom(src => src.ActualDeliveryTime));
            .ForMember(dest => dest.DeliveryStatus, opt => opt.Ignore())
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.OrderStatus, opt => opt.Ignore())
            .ForMember(dest => dest.CourierId, opt => opt.Ignore())
            .ForMember(dest => dest.TotalQuantity, opt => opt.Ignore())
            .ForMember(dest => dest.TotalPrice, opt => opt.Ignore())
            .ForMember(dest => dest.ShippingAddress, opt => opt.Ignore())
            .ForMember(dest => dest.EstimatedDeliveryTime, opt => opt.Ignore())
            .ForMember(dest => dest.ActualDeliveryTime, opt => opt.Ignore());

            CreateMap<DataAccess.Domain.Domain.Entities.Delivery, DeliveryResponse>();
            CreateMap<CreateDeliveryRequest, CreateDeliveryDto>();
            //CreateMap<DeliveryModel, DeliveryDto>();
            //CreateMap<CreateDeliveryModel, CreateDeliveryDto>();
            //CreateMap<DataAccess.Domain.Domain.Entities.Delivery, CreateDeliveryDto>();
            //CreateMap<UpdateDeliveryModel, updateDeliveryDto>();
        }
    }
}