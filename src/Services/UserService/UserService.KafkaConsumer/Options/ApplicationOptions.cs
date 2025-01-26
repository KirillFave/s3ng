using SharedLibrary.Common.Kafka;

namespace UserService.KafkaConsumer.Options;

public class ApplicationOptions
{
    public KafkaOptions KafkaOptions { get; set; }

    public string GroupId { get; set; }
}