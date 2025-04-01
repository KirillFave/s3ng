using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OrderService.Database;
using SharedLibrary.OrderService.Dto;
using SharedLibrary.OrderService.Models;

namespace OrderService.Repositories;

public class OrderRepository
{
    protected readonly DatabaseContext _databaseContext;

    public OrderRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public IEnumerable<Order> GetAllAsync()
    {
        return _databaseContext.Orders.AsEnumerable();
    }

    public async Task<Order?> GetByIdAsync(Guid guid)
    {
        return await _databaseContext.Orders
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == guid);
    }

    public async Task<bool> AddAsync(Order order)
    {
        EntityEntry<Order> entityEntry = await _databaseContext.Orders.AddAsync(order);

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();
        return stateEntriesWritten > 0;
    }

    public async Task<OperationResult> CancelAsync(Guid id)
    {
        Order? orderToUpdate = await _databaseContext.Orders.FindAsync(id);
        if (orderToUpdate is null)
        {
            return OperationResult.NotEntityFound;
        }

        orderToUpdate.IsCanceled = true;

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();
        return stateEntriesWritten > 0 ? OperationResult.Success : OperationResult.NotChangesApplied;
    }
}
