using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using DeliveryService.Kafka.Models;
using DeliveryService.Kafka.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DeliveryService.Kafka.Consumers;

public abstract class ConsumerBackgroundService<TKey, TValue> : BackgroundService where TValue : IKafkaMessage
{
    private ILogger _logger;
    private BaseConsumer<TKey, TValue> _baseConsumer;
    protected abstract string TopicName { get; }

    public ConsumerBackgroundService(
        ILogger logger,
        ApplicationOptions applicationOptions)
    {
        _baseConsumer = new BaseConsumer<TKey, TValue>(applicationOptions.KafkaOptions, logger, applicationOptions.GroupId);
        _logger = logger;

    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _baseConsumer.Consumer.Subscribe(TopicName);
        _logger.LogInformation($"Start consumer topic {TopicName}");
        while (!stoppingToken.IsCancellationRequested)
        {
            await Consume(stoppingToken);
        }

        _baseConsumer.Consumer.Unsubscribe();
        _logger.LogInformation($"Stop consumer topic {TopicName}");
    }

    private async Task Consume(CancellationToken cancellationToken)
    {
        ConsumeResult<TKey, TValue> message = null;
        try
        {
            message = _baseConsumer.Consumer.Consume(TimeSpan.FromSeconds(10)); //можно подставлять cancellationToken но тогда под капотом он подставит свое прерывание 5 сек и непредсказуемое поведение

            if (message is null)
            {
                await Task.Delay(100, cancellationToken);
                return;
            }

            await HandleAsync(message, cancellationToken);
            _baseConsumer.Consumer.Commit();
        }
        catch (Exception e)
        {
            var key = message is null ? message.Message.Key.ToString() : "No key";
            var value = message is null ? message.Message.Value.ToString() : "No value";
            _logger.LogError(e, $"Error process message with key {key}, value {value}");
        }
    }

    protected abstract Task HandleAsync(ConsumeResult<TKey, TValue> message, CancellationToken cancellationToken);

    public override void Dispose()
    {
        base.Dispose();
    }
}
