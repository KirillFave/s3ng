using OrderService.Models;

namespace OrderService.Repositories;

public interface IRepository<T>
    where T : BaseEntity
{
    Task<List<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid guid);

    Task<bool> AddAsync(T entity);

    Task<OperationResult> UpdateAsync(T entity);

    Task<OperationResult> DeleteAsync(Guid guid);
}