using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SharedLibrary.Common.Kafka;
using SharedLibrary.Common.Kafka.Messages;
using ILogger = Serilog.ILogger;

namespace OrderService.Producers;

public class OrderCanceledProducer : BaseProducer<string, OrderCanceledMessage>
{
    private const string TopicName = "order_canceled_events";

    public OrderCanceledProducer(
        IOptions<KafkaOptions> applicationOptions, ILogger logger) :
        base(applicationOptions.Value, logger.ForContext<OrderCanceledProducer>())
    {
    }

    public async Task ProduceAsync(string key, OrderCanceledMessage message)
    {
        try
        {
            await Producer.ProduceAsync(
                TopicName,
                new Message<string, OrderCanceledMessage>
                {
                    Key = key,
                    Value = message
                }
            );
            Logger.Information($"Message for order with id {key} sent");
        }
        catch (Exception e)
        {
            Logger.Error($"produce error {e.Message}");
            throw;
        }
    }
}
