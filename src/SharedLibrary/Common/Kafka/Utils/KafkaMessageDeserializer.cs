using System.Text.Json;
using Confluent.Kafka;
using SharedLibrary.Common.Kafka.Messages;

namespace SharedLibrary.Common.Kafka.Utils;

public class KafkaMessageDeserializer<T> : IDeserializer<T> where T : IKafkaMessage
{
    public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        return JsonSerializer.Deserialize<T>(data.ToArray());
    }
}
