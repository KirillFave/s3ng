using SharedLibrary.OrderService.Models;

namespace SharedLibrary.OrderService.Dto;

public class GetOrderResponseDto
{
    public required Guid Id { get; set; }
    public required Guid UserGuid { get; set; }
    public required IEnumerable<GetOrderItemResponseDto> Items { get; set; }
    public required OrderStatus Status { get; set; }
    public required PaymentType PaymentType { get; set; }
    public string? ShipAddress { get; set; }
    public required DateTime CreatedTimestamp { get; set; }
    public required bool IsCanceled { get; set; }
}
