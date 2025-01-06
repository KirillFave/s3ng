using OrderService.Models;

namespace OrderService.Dto;

public class GetOrderResponseDto
{
    public required Guid Guid { get; set; }
    public required Guid UserId { get; set; }
    public required List<Guid> ItemGuids { get; set; }
    public required OrderStatus Status { get; set; }
    public required PaymentType PaymentType { get; set; }
    public string? ShipAddress { get; set; }
    public required DateTime CreatedTimestamp { get; set; }
}
