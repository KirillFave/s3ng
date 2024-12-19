using UserService.Domain.Entities;

namespace UserService.Infrastructure.Repository;

public interface IUserRepository
{
    /// <summary>
    /// Запросить все сущности в базе.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены. </param>
    /// <returns> Список сущностей. </returns>
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"></param>
    /// <returns> Cущность. </returns>
    Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Получить сущность по AuthenticationId.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <param name="cancellationToken"></param>
    /// <returns> Cущность. </returns>
    Task<User> GetByAuthenticationIdAsync(Guid authenticationId, CancellationToken cancellationToken);

    /// <summary>
    /// Добавить в базу одну сущность.
    /// </summary>
    /// <param name="entity"> Сущность для добавления. </param>
    /// <returns> Добавленная сущность. </returns>
    Task<User> AddAsync(User entity, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="id"> Id удалённой сущности. </param>
    /// <returns> Была ли сущность удалена. </returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить сущность.
    /// </summary>
    /// <param name="entity"> Сущность для обновления. </param>
    /// <returns> Обновленная сущность. </returns>
    Task<User> UpdateAsync(User entity, CancellationToken cancellationToken);

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
