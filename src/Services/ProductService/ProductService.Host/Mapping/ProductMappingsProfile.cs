using AutoMapper;
using s3ng.ProductService.Services.Contracts.Product;
using s3ng.ProductService.Host.Models.Product;

namespace s3ng.ProductService.Host.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности товара.
    /// </summary>
    public class ProductMappingsProfile : Profile
    {
        public ProductMappingsProfile()
        {
            CreateMap<ProductDto, ProductModel>();
            CreateMap<CreatingProductModel, CreatingProductDto>();
            CreateMap<UpdatingProductModel, UpdatingProductDto>();
        }
    }
}
