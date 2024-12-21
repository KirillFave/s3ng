namespace SharedLibrary.ProductService.Models
{
    public class ProductModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required Guid SellerId { get; set; }
    }
}
