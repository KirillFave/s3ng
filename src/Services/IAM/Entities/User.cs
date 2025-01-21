using SharedLibrary.IAM.Enums;

namespace IAM.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    internal class User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        internal required Guid Id { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        internal required string Login { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        internal required string PasswordHash { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        internal required RoleType Role { get; set; }
    }
}
