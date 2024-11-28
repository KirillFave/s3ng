using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OrderService.Database;
using OrderService.Models;

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
            .FirstOrDefaultAsync(order => order.Guid == guid);
    }

    public async Task<bool> AddAsync(Order order)
    {
        EntityEntry<Order> entityEntry = await _databaseContext.Orders.AddAsync(order);

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();
        return stateEntriesWritten > 0;
    }

    public async Task<OperationResult> DeleteAsync(Guid guid)
    {
        Order? entityToUpdate = await _databaseContext.Orders.FindAsync(guid);
        if (entityToUpdate is null)
        {
            return OperationResult.NotEntityFound;
        }

        _databaseContext.Orders.Remove(entityToUpdate);

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();
        return stateEntriesWritten > 0 ? OperationResult.Success : OperationResult.NotChangesApplied;
    }

    public async Task<OperationResult> UpdateAsync(
        Order order,
        bool isUpdatePaymentType)
    {
        Order? orderToUpdate = await _databaseContext.Orders.FindAsync(order.Guid);
        if (orderToUpdate is null)
        {
            return OperationResult.NotEntityFound;
        }

        if (orderToUpdate.PaymentType == order.PaymentType &&
            orderToUpdate.Status == order.Status &&
            orderToUpdate.Items == order.Items)
        {
            return OperationResult.NotModified;
        }

        if (isUpdatePaymentType)
        {
            orderToUpdate.PaymentType = order.PaymentType;
        }

        if (order.ShipAddress is not null)
        {
            orderToUpdate.ShipAddress = order.ShipAddress;
        }

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();
        return stateEntriesWritten > 0 ? OperationResult.Success : OperationResult.NotChangesApplied;
    }
}
