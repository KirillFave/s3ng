using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using DeliveryService.Kafka.Models;
using DeliveryService.Kafka.Options;
using Microsoft.Extensions.Logging;

namespace DeliveryService.Kafka.Consumers;

public class NewOrderConsumer : ConsumerBackgroundService<string, OrderCreatedMessage>
{
    private readonly ILogger<NewOrderConsumer> _logger;

    public NewOrderConsumer(
        ILogger<NewOrderConsumer> logger,
        ApplicationOptions applicationOptions)
        : base(logger, applicationOptions)
    {
        TopicName = "order_events";
        //TopicName = "mytopic4";
        _logger = logger;
    }

    protected override string TopicName { get; }

    protected override Task HandleAsync(ConsumeResult<string, OrderCreatedMessage> message, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Order {message.Message.Key} received");
        return Task.CompletedTask;
    }
}
