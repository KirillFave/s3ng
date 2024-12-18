namespace WebHost.Dto
{
    /// <summary>
    /// Запрос на авторизацию
    /// </summary>
    public class AuthenticationRequestDto
    {
        /// <summary>
        /// Логин
        /// </summary>
        public required string Login { get; set; } = null!;

        /// <summary>
        /// Пароль
        /// </summary>
        public required string Password { get; set; } = null!;
    }
}
