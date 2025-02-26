namespace SharedLibrary.OrderService.Dto;

public class CreateOrderItemDto
{
    public Guid ProductId { get; set; }
    public decimal PricePerUnit { get; set; }
    public int Count { get; set; }
}
