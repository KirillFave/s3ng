using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Infrastructure.EFCore;

namespace UserService.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    protected readonly UserServiceContext Context;
    private readonly DbSet<User> _entityUserSet;

    public UserRepository(UserServiceContext context)
    {
        Context = context;
        _entityUserSet = Context.Set<User>();
    }

    /// <summary>
    /// Добавить в базу одну сущность.
    /// </summary>
    /// <param name="entity"> Сущность для добавления. </param>
    /// <returns> Добавленная сущность. </returns>
    public async Task<User> AddAsync(User entity, CancellationToken cancellationToken)
    {
        return (await _entityUserSet.AddAsync(entity, cancellationToken)).Entity;
    }

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="id"> Id удаляемой сущности. </param>
    /// <returns> Была ли сущность удалена. </returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var deleteEntity = await _entityUserSet.FindAsync(id, cancellationToken);
        if (deleteEntity == null)
        {
            return true;
        }

        _entityUserSet.Remove(deleteEntity);
        return true;
    }

    /// <summary>
    /// Запросить все сущности в базе.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Список сущностей. </returns>
    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _entityUserSet.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"></param>
    /// <returns> Cущность. </returns>
    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _entityUserSet.FindAsync(id, cancellationToken);
    }

    /// <summary>
    /// Получить сущность по AuthenticationId.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"></param>
    /// <returns> Cущность. </returns>
    public async Task<User> GetByAuthenticationIdAsync(Guid authenticationId, CancellationToken cancellationToken)
    {
        return await _entityUserSet.FindAsync(authenticationId, cancellationToken);
    }

    /// <summary>
    /// Обновить в базе сущность.
    /// </summary>
    /// <param name="entity"> Сущность для обновления. </param>
    /// <returns> Обновленная сущность. </returns>
    public async Task<User> UpdateAsync(User entity, CancellationToken cancellationToken)
    {
        _entityUserSet.Update(entity);
        return await _entityUserSet.FindAsync(entity.Id, cancellationToken);
    }


    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await Context.SaveChangesAsync(cancellationToken);
    }
}
