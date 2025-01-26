using Confluent.Kafka;
using SharedLibrary.Common.Kafka.Messages;
using SharedLibrary.Common.Kafka.Utils;
using ILogger = Serilog.ILogger;

namespace SharedLibrary.Common.Kafka
{
    /// <summary>
    /// Базовая реализация продюсера
    /// </summary>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Тип значение</typeparam>
    public class BaseProducer<TKey, TValue> where TValue : IKafkaMessage
    {
        protected readonly ILogger Logger;

        public IProducer<TKey, TValue> Producer { get; }


        public BaseProducer(KafkaOptions kafkaOptions, ILogger logger)
        {
            Logger = logger;
            var producerConfig = new ProducerConfig()
            {
                BootstrapServers = kafkaOptions.BootstrapServers,
                Partitioner = Partitioner.Consistent,
            };
            try
            {
                var producerBuilder = new ProducerBuilder<TKey, TValue>(producerConfig);
                Producer = producerBuilder
                    .SetErrorHandler((_, error) => Logger.Error(error.Reason))
                    .SetLogHandler((_, message) => Logger.Information(message.Message))
                    .SetKeySerializer((ISerializer<TKey>)Serializers.Utf8)
                    .SetValueSerializer(new KafkaMessageSerializer<TValue>())
                    .Build();
            }
            catch (Exception ex) 
            {
                Logger.Error($"producer build error {ex}");
                throw;
            }
        }
    }
}
