using s3ng.ProductService.Core.Abstractions;

namespace s3ng.ProductService.Core.Domain.Managment
{
    public class Product : IEntity<Guid>
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid SellerId { get; set; }
        public DateTime TimeCreated { get; }
        public DateTime TimeModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
