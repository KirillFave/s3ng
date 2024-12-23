using s3ng.ProductService.Core.Abstractions;

namespace s3ng.ProductService.Core.Domain.Managment
{
    public class Product : IEntity<Guid>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public required decimal Price { get; set; }
        public required Guid SellerId { get; set; }
        public required DateTime TimeCreated { get; set; }
        public required DateTime TimeModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
