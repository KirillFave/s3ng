using MongoDB.Driver;
using ProductService.Domain;

namespace ProductService.Infrastructure
{
    public class MongoProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _collection;

        public MongoProductRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Product>("products");
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken ct = default) => await _collection.Find(_ => true).ToListAsync(cancellationToken: ct);

        public async Task<Product?> GetByIdAsync(string id, CancellationToken ct = default) => await _collection.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken: ct);

        public Task CreateAsync(Product product, CancellationToken ct = default) => _collection.InsertOneAsync(product, cancellationToken: ct);

        public Task UpdateAsync(Product product, CancellationToken ct = default) => _collection.ReplaceOneAsync(p => p.Id == product.Id, product, cancellationToken: ct);

        public Task DeleteAsync(string id, CancellationToken ct = default) => _collection.DeleteOneAsync(p => p.Id == id, cancellationToken: ct);
    }
}
