namespace WebHost.Dto.IAM
{
    /// <summary>
    /// Запрос на регистрацию
    /// </summary>
    public sealed class RegistrationRequestDto
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
