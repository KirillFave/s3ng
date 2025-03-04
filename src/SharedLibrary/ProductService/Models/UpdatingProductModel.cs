using System.Xml.Linq;

namespace SharedLibrary.ProductService.Models
{
    public class UpdatingProductModel
    {
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public required Guid SellerId { get; set; }

        public override string ToString()
        {
            return $"Description={Description}, Price={Price}, SellerId={SellerId}";
        }
    }
}
