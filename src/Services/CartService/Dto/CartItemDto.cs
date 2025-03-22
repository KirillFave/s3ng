namespace CartService.Dto;

public class CartItemDto
{
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public required decimal Price { get; set; }
    public required int Quantity { get; set; }
}