namespace SharedLibrary.Common.Kafka.Messages;

public class OrderCanceledMessage : IKafkaMessage
{
    public Guid Id { get; set; }
}
