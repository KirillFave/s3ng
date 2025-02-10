namespace SharedLibrary.OrderService.Dto;

public class GetOrderItemResponseDto
{
    public required Guid Id { get; set; }
    public required Guid OrderId { get; set; }
    public required Guid ProductGuid { get; set; }
    public required decimal PricePerUnit { get; set; }
    public required int Count { get; set; }
}
