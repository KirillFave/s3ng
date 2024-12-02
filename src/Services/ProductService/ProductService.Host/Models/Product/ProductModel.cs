namespace s3ng.ProductService.Host.Models.Product
{
    public class ProductModel
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required Guid SellerId { get; set; }
    }
}
