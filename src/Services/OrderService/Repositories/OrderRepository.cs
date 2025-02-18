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

    public async Task<OperationResult> DeleteAsync(Guid guid)
    {
        Order? entityToDelete = await _databaseContext.Orders
                                                      .FirstOrDefaultAsync(order => order.Id.Equals(guid));

        if (entityToDelete is null)
        {
            return OperationResult.NotEntityFound;
        }

        _databaseContext.Orders.Remove(entityToDelete);

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();

        return stateEntriesWritten > 0 ? OperationResult.Success : OperationResult.NotChangesApplied;
    }

    public async Task<OperationResult> UpdateAsync(Order order)
    {
        Order? orderToUpdate = await _databaseContext.Orders.FindAsync(order.Id);
        if (orderToUpdate is null)
        {
            return OperationResult.NotEntityFound;
        }

        if (orderToUpdate.PaymentType == order.PaymentType &&
            orderToUpdate.Status == order.Status)
        {
            return OperationResult.NotModified;
        }

        if (order.PaymentType != PaymentType.Undefined)
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

    public async Task<OperationResult> DeleteOrderItemAsync(Order order)
    {
        Order? orderToUpdate = await _databaseContext.Orders.FindAsync(order.Id);
        if (orderToUpdate is null)
        {
            return OperationResult.NotEntityFound;
        }

        if (order.Items.Count == orderToUpdate.Items.Count)
        {
            return OperationResult.NotModified;
        }

        orderToUpdate.Items = order.Items;

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();
        return stateEntriesWritten > 0 ? OperationResult.Success : OperationResult.NotChangesApplied;
    }
}
