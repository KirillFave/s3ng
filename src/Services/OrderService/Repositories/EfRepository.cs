using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Net.Http.Headers;
using OrderService.Database;
using OrderService.Models;
using System.Reflection;

namespace OrderService.Repositories;

public class EfRepository<T>
    : IRepository<T>
    where T : BaseEntity
{
    private readonly DatabaseContext _databaseContext;

    public EfRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return _databaseContext.Set<T>().ToList();
    }

    public async Task<T?> GetByIdAsync(Guid guid)
    {
        DbSet<T> entitySet = _databaseContext.Set<T>();
        T? entity = await entitySet.FindAsync(guid);
        return entity;
    }

    public async Task<bool> AddAsync(T entity)
    {
        DbSet<T> entitySet = _databaseContext.Set<T>();
        EntityEntry<T> entityEntry = await entitySet.AddAsync(entity);

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();
        return stateEntriesWritten > 0;
    }

    public async Task<OperationResult> UpdateAsync(T entity)
    { 
        T? entityToUpdate = await _databaseContext.Set<T>().FindAsync(entity.Guid);
        if (entityToUpdate is null)
        {
            return OperationResult.NotEntityFound;
        }

        // Универсальная логика обновления данных с помощью рефлексии
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (property.CanWrite)
            {
                object? newValue = property.GetValue(entity);
                property.SetValue(entityToUpdate, newValue);
            }
        }

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();
        return stateEntriesWritten > 0 ? OperationResult.Success : OperationResult.NotChangesApplied;
    }

    public async Task<OperationResult> DeleteAsync(Guid guid)
    {
        DbSet<T> entitySet = _databaseContext.Set<T>();
        T? entityToUpdate = await entitySet.FindAsync(guid);
        if (entityToUpdate is null)
        {
            return OperationResult.NotEntityFound;
        }

        entitySet.Remove(entityToUpdate);

        int stateEntriesWritten = await _databaseContext.SaveChangesAsync();
        return stateEntriesWritten > 0 ? OperationResult.Success : OperationResult.NotChangesApplied;
    }
}