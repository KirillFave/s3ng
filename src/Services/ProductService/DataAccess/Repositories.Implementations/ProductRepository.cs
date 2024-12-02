using Microsoft.EntityFrameworkCore;
using s3ng.ProductService.Core.Domain.Managment;
using s3ng.ProductService.DataAccess.EntityFramework;
using s3ng.ProductService.Services.Repositories.Abstractions;

namespace s3ng.ProductService.DataAccess.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<Product> _productSet;

        public ProductRepository(DatabaseContext context)
        {
            _context = context;
            _productSet = _context.Set<Product>();
        }

        public Product Get(Guid id)
        {
            return _productSet.Find(id);
        }

        public async Task<Product> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _productSet.FindAsync(id);
        }

        public IQueryable<Product> GetAll(bool asNoTracking = false)
        {
            return asNoTracking ? _productSet.AsNoTracking() : _productSet;
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default, bool asNoTracking = false)
        {
            return await GetAll().ToListAsync(cancellationToken);
        }

        public Product Add(Product product)
        {
            var productObj = _productSet.Add(product);
            return productObj.Entity;
        }

        public async Task<Product> AddAsync(Product product)
        {
            return (await _productSet.AddAsync(product)).Entity;
        }

        public void Update(Product product)
        {
            product.TimeModified = DateTime.Now;
            _productSet.Entry(product).State = EntityState.Modified;
        }

        public bool Delete(Product product)
        {
            if (product is null)
            {
                return false;
            }

            _productSet.Remove(product);
            _productSet.Entry(product).State = EntityState.Deleted;

            return true;
        }

        public bool Delete(Guid id)
        {
            Product product = Get(id);

            return Delete(product);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
