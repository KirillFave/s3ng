using SharedLibrary.OrderService.Models;

namespace SharedLibrary.OrderService.Dto;

public class CreateOrderItemDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public decimal PricePerUnit { get; set; }
    public int Count { get; set; }
}
