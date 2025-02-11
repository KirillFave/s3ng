namespace WebHost.Dto.IAM
{
    /// <summary>
    /// Запрос на авторизацию
    /// </summary>
    public class AuthenticationRequestDto
    {
        /// <summary>
        /// Почта
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public required string Password { get; set; }
    }
}
