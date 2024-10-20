namespace s3ng.ProductService.Host.Models.Product
{
    public class CreatingProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid SellerId { get; set; }
    }
}
