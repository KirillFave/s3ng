namespace s3ng.ProductService.Services.Contracts.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid SellerId { get; set; }
    }
}
