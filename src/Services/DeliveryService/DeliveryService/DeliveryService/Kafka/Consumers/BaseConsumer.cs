using Confluent.Kafka;
using Confluent.Kafka.Admin;
using DeliveryService.Kafka.Models;
using DeliveryService.Kafka.Options;
using DeliveryService.Kafka.Utils;
using Microsoft.Extensions.Logging;
//using ILogger = Serilog.ILogger;

namespace DeliveryService.Kafka.Consumers;
public sealed class BaseConsumer<TKey, TValue> where TValue : IKafkaMessage
{
    private ILogger _logger;

    public IConsumer<TKey, TValue> Consumer { get; }

    public BaseConsumer(KafkaOptions kafkaOptions, ILogger logger, string groupId)
    {
        _logger = logger;
        var consumerConfig = new ConsumerConfig
        {
            GroupId = groupId,
            BootstrapServers = kafkaOptions.BootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
        };
        ConsumerBuilder<TKey, TValue> consumerBuilder = new ConsumerBuilder<TKey, TValue>(consumerConfig);
        Consumer = consumerBuilder
            .SetErrorHandler((_, error) => _logger.LogError(error.Reason))
            .SetLogHandler((_, message) =>
            {
                _logger.LogInformation(message.Message);
            })
            .SetKeyDeserializer((IDeserializer<TKey>)Deserializers.Utf8)
            .SetValueDeserializer(new KafkaMessageDeserializer<TValue>())
            .Build();
    }
}
