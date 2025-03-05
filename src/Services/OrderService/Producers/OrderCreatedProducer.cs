using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SharedLibrary.Common.Kafka;
using SharedLibrary.Common.Kafka.Messages;
using ILogger = Serilog.ILogger;

namespace OrderService.Producers
{
    public class OrderCreatedProducer : BaseProducer<string, OrderCreatedMessage>
    {
        private const string TopicName = "order_created_events";

        public OrderCreatedProducer(
            IOptions<KafkaOptions> applicationOptions, ILogger logger) :
            base(applicationOptions.Value, logger.ForContext<OrderCreatedProducer>())
        {
        }

        public async Task ProduceAsync(string key, OrderCreatedMessage message)
        {
            try
            {
                await Producer.ProduceAsync(
                    TopicName, 
                    new Message<string, OrderCreatedMessage>
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
}
