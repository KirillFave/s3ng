using SharedLibrary.Common.Kafka;

namespace SharedLibrary.IAM.Messages
{
    /// <summary>
    /// Сообщение о регистрации пользователя
    /// </summary>
    public class UserRegistredMessage : IKafkaMessage
    {
        /// <summary>
        /// ИД
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Время регистрации
        /// </summary>
        public DateTimeOffset RegistredAt { get; set; }
    }
}