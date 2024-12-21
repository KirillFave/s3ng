namespace SharedLibrary.ProductService.Models
{
    public class UpdatingProductModel
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid SellerId { get; set; }
    }
}
