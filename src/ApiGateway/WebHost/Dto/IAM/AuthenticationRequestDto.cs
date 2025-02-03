namespace WebHost.Dto.IAM
{
    /// <summary>
    /// Запрос на авторизацию
    /// </summary>
    public class AuthenticationRequestDto
    {
        /// <summary>
        /// Логин
        /// </summary>
        public required string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public required string Password { get; set; }
    }
}
