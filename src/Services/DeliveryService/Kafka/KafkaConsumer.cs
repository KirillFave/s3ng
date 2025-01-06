using Confluent.Kafka;
using DeliveryService.Models;
using Newtonsoft.Json;
using DeliveryService.Data;

namespace DeliveryService.Kafka
{
    public class KafkaConsumer(IServiceScopeFactory scopeFactory) : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            return Task.Run(() =>
            {
                _ = ConsumeAsync("order-topic", stoppingToken);
            }, stoppingToken);
        }

        public async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "order-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<string, string>(config).Build();

            consumer.Subscribe(topic);
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(stoppingToken);

                var order = JsonConvert.DeserializeObject<OrderMessage>(consumeResult.Message.Value);
                using var scope = scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<DeliveryDBContext>();

                var product = await dbContext.Deliveries.FindAsync(order.TotalQuantity);
                if (order != null)
                {
                    order.TotalQuantity -= order.TotalQuantity;
                    await dbContext.SaveChangesAsync();
                }
            }
            consumer.Close();
        }
    }
}
