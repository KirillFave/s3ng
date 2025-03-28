using Confluent.Kafka;
using DeliveryService.Kafka.Models;
using DeliveryService.Kafka.Options;
using DeliveryService.Kafka.Utils;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace DeliveryService.Kafka.Consumers;
public sealed class BaseConsumer<TKey, TValue> where TValue : IKafkaMessage
{
    private ILogger _logger;
    private KafkaOptions _kafkaOptions;
    public IConsumer<TKey, TValue> Consumer { get; }

    public BaseConsumer(KafkaOptions kafkaOptions, ILogger logger, string groupId)
    {
        _logger = logger;
        _kafkaOptions = kafkaOptions;

        {
            var consumerConfig = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = _kafkaOptions.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
            };
            ConsumerBuilder<TKey, TValue> consumerBuilder = new ConsumerBuilder<TKey, TValue>(consumerConfig);
            Consumer = consumerBuilder
                .SetErrorHandler((_, error) => _logger.Error(error.Reason))
                .SetLogHandler((_, message) =>
                {
                    _logger.Information(message.Message);
                })
                .SetKeyDeserializer((IDeserializer<TKey>)Deserializers.Utf8)
                .SetValueDeserializer(new KafkaMessageDeserializer<TValue>())
                .Build();
        }
        catch (Exception ex)
        {
            _logger.Error($"BaseConsumer error {ex}");
            throw;
        }
    }
    public async Task EnsureTopicExists(string topicName)
    {
        var adminConfig = new AdminClientConfig { BootstrapServers = _kafkaOptions.BootstrapServers };

        using var adminClient = new AdminClientBuilder(adminConfig).Build();

        try
        {
            var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(5));

            if (metadata.Topics.Any(t => t.Topic == topicName && !t.Error.IsError))
            {
                _logger.Information($"Topic '{topicName}' already exists");
                return;
            }

            var topicSpecification = new TopicSpecification
            {
                Name = topicName
            };

            await adminClient.CreateTopicsAsync(new[] { topicSpecification });
            _logger.Information($"Topic '{topicName}' successfully created");
        }
        catch (Exception ex)
        {
            _logger.Error($"Error for creation '{topicName}': {ex}");
        }
    }

}
