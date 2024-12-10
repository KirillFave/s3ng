namespace s3ng.ProductService.Host.Models.Product
{
    public class CreatingProductModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required Guid SellerId { get; set; }
    }
}
