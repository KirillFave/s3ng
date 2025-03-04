using SharedLibrary.OrderService.Dto;
using SharedLibrary.OrderService.Models;

namespace SharedLibrary.Common.Kafka.Messages;

public class OrderCreatedMessage : IKafkaMessage
{
    public Guid Id { get; set; }
    public Guid UserGuid { get; set; }
    public IEnumerable<GetOrderItemResponseDto> Items { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentType PaymentType { get; set; }
    public string? ShipAddress { get; set; }
    public DateTime CreatedTimestamp { get; set; }
}
