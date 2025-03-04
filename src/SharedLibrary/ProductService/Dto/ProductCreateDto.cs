using Microsoft.AspNetCore.Http;

namespace SharedLibrary.ProductService.Dto
{
    /// <summary>
    /// Запрос на создание толвара
    /// </summary>
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
    }
}
