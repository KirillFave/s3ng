namespace SharedLibrary.ProductService.Models
{
    public class CreatingProductModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required Guid SellerId { get; set; }

        public override string ToString()
        {
            return $"Name={Name}, Description={Description}, Price={Price}, SellerId={SellerId}";
        }
    }
}
