namespace s3ng.ProductService.Services.Contracts.Product
{
    public class UpdatingProductDto
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid SellerId { get; set; }
    }
}