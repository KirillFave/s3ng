namespace SharedLibrary.Common.Kafka
{
    /// <summary>
    /// Опции кафки
    /// </summary>
    public class KafkaOptions
    {
        public const string Kafka = nameof(KafkaOptions);
        /// <summary>
        /// Серверы начальной загрузки
        /// </summary>
        public string BootstrapServers { get; set; }
    }
}
