using IAM.Entities;

namespace IAM.Seedwork.Abstractions
{
    /// <summary>
    /// Контракт токен провайдера
    /// </summary>
    internal interface ITokenProvider
    {
        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Токен</returns>
        public string GenerateToken(User user);

        /// <summary>
        /// Сгенерировать обновляющий токен
        /// </summary>
        /// <returns>Обновляющий токен</returns>
        public string GenerateRefreshToken();
    }
}
