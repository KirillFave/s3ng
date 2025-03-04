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

        public async Task<List<Product>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetByIdAsync(string id) => await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();

        public Task CreateAsync(Product product) => _collection.InsertOneAsync(product);

        public Task UpdateAsync(Product product) =>
            _collection.ReplaceOneAsync(p => p.Id == product.Id, product);

        public Task DeleteAsync(string id) => _collection.DeleteOneAsync(p => p.Id == id);
    }
}
