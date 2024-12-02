using AutoMapper;
using s3ng.ProductService.Services.Contracts.Product;
using s3ng.ProductService.Core.Domain.Managment;

namespace s3ng.ProductService.ProductService.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности товара.
    /// </summary>
    public class ProductMappingsProfile : Profile
    {
        public ProductMappingsProfile()
        {
            CreateMap<Product, ProductDto>();

            CreateMap<CreatingProductDto, Product>()
                .ForMember(product => product.Id, map => map.Ignore())
                .ForMember(product => product.TimeCreated, map => map.Ignore())
                .ForMember(product => product.TimeModified, map => map.Ignore())
                .ForMember(product => product.IsDeleted, map => map.Ignore());

            CreateMap<UpdatingProductDto, Product>()
                .ForMember(product => product.Id, map => map.Ignore())
                .ForMember(product => product.Name, map => map.Ignore())
                .ForMember(product => product.TimeCreated, map => map.Ignore())
                .ForMember(product => product.TimeModified, map => map.Ignore())
                .ForMember(product => product.IsDeleted, map => map.Ignore());
        }
    }
}
