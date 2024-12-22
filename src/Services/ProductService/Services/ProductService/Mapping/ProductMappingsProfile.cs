using AutoMapper;
using ProductService.Core.Domain.Managment;
using SharedLibrary.ProductService.Models;

namespace ProductService.ProductService.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности товара.
    /// </summary>
    public class ProductMappingsProfile : Profile
    {
        public ProductMappingsProfile()
        {
            CreateMap<Product, ProductModel>();

            CreateMap<CreatingProductModel, Product>()
                .ForMember(product => product.Id, map => map.Ignore())
                .ForMember(product => product.TimeCreated, map => map.Ignore())
                .ForMember(product => product.TimeModified, map => map.Ignore())
                .ForMember(product => product.IsDeleted, map => map.Ignore());

            CreateMap<UpdatingProductModel, Product>()
                .ForMember(product => product.Id, map => map.Ignore())
                .ForMember(product => product.Name, map => map.Ignore())
                .ForMember(product => product.TimeCreated, map => map.Ignore())
                .ForMember(product => product.TimeModified, map => map.Ignore())
                .ForMember(product => product.IsDeleted, map => map.Ignore());
        }
    }
}
