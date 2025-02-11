using IAM.DAL;
using IAM.Entities;
using Microsoft.EntityFrameworkCore;

namespace IAM.Repositories
{
    /// <summary>
    /// Репозиторий для работы с пользователями
    /// </summary>
    /// <param name="databaseContext"></param>
    internal class UserRepository(DatabaseContext databaseContext)
    {
        private readonly DatabaseContext _databaseContext = databaseContext;

        /// <summary>
        /// Получить пользователя по логину
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="ct">Токен отмены</param>
        /// <returns>Пользователь</returns>
        public async Task<User?> GetByLoginAsync(string login, CancellationToken ct = default) => await _databaseContext.Users.FirstOrDefaultAsync(x => x.Login == login, ct);

        /// <summary>
        /// Сохранить нового пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="ct">Токен отмены</param>
        public async Task AddAsync(User user, CancellationToken ct = default) => await _databaseContext.Users.AddAsync(user, ct);

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        /// <param name="ct">Токен отмены</param>
        public async Task SaveAsync(CancellationToken ct = default) => await _databaseContext.SaveChangesAsync(ct);
    }
}
