namespace OrderService.Dto;

public class GetOrderItemResponseDto
{
    public required Guid Guid { get; set; }
    public required Guid OrderGuid { get; set; }
    public required Guid ProductGuid { get; set; }
    public required decimal PricePerUnit { get; set; }
    public required int Count { get; set; }
    public required decimal TotalPrice { get; set; }
}
