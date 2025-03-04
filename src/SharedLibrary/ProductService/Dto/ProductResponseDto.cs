namespace SharedLibrary.ProductService.Dto
{
    /// <summary>
    /// Ответ о товаре
    /// </summary>
    public class ProductResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
