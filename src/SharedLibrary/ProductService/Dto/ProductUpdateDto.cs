using Microsoft.AspNetCore.Http;

namespace SharedLibrary.ProductService.Dto
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
    }
}
