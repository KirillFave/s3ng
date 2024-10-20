namespace s3ng.ProductService.Host.Models.Product
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid SellerId { get; set; }
    }
}
