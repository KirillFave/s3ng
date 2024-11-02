using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OrderService.Database;
using OrderService.Models;

namespace OrderService.Repositories;

public class OrderItemRepository
{
    protected readonly DatabaseContext _databaseContext;

    public OrderItemRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public IEnumerable<OrderItem> GetAllAsync()
    {
        return _databaseContext.OrderItems.AsEnumerable();
    }

    public async Task<OrderItem?> GetByIdAsync(Guid guid)
    {
        return await _databaseContext.OrderItems
            .Include(orderItem => orderItem.Order)
            .FirstOrDefaultAsync(orderItem => orderItem.Guid == guid);
    }

    public async Task<bool> AddAsync(OrderItem orderItem)
    {
        EntityEntry<OrderItem> entityEntry = await _databaseContext.OrderItems.AddAsync(orderItem);

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();
        return stateEntriesWritten > 0;
    }

    public async Task<OperationResult> DeleteAsync(Guid guid)
    {
        OrderItem? entityToUpdate = await _databaseContext.OrderItems.FindAsync(guid);
        if (entityToUpdate is null)
        {
            return OperationResult.NotEntityFound;
        }

        _databaseContext.OrderItems.Remove(entityToUpdate);

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();
        return stateEntriesWritten > 0 ? OperationResult.Success : OperationResult.NotChangesApplied;
    }
}
