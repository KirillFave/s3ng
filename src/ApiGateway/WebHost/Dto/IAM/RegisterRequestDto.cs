using SharedLibrary.IAM.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace WebHost.Dto.IAM
{
    /// <summary>
    /// Запрос на регистрацию
    /// </summary>
    public sealed class RegistrationRequestDto
    {
        /// <summary>
        /// Почта
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        [SwaggerIgnore]
        public RoleType? Role { get; set; }
    }
}
