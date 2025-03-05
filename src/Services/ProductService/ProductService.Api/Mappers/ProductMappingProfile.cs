using AutoMapper;
using ProductService.Domain;
using SharedLibrary.ProductService.Dto;

namespace ProductService.Api.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
