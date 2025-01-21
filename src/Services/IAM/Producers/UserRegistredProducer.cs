using Confluent.Kafka;
using SharedLibrary.Common.Kafka;
using SharedLibrary.IAM.Messages;
using ILogger = Serilog.ILogger;

namespace IAM.Producers
{
    /// <summary>
    /// Продюсер событий регистрации пользователя
    /// </summary>
    public class UserRegistredProducer : BaseProducer<string, UserRegistredMessage>
    {
        private const string TopicName = "registration_events";

        public UserRegistredProducer(
            KafkaOptions applicationOptions,
            ILogger logger) :
            base(applicationOptions, logger)
        { }

        public async Task ProduceAsync(string key, UserRegistredMessage message, CancellationToken cancellationToken)
        {
            try
            {
                await Producer.ProduceAsync(TopicName, new Message<string, UserRegistredMessage>
                {
                    Key = key,
                    Value = message
                },
                cancellationToken);
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
