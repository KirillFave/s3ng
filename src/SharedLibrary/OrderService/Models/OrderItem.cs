namespace SharedLibrary.OrderService.Models;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public virtual Order Order { get; set; }
    public Guid ProductGuid { get; set; }
    public decimal PricePerUnit { get; set; }
    public int Count { get; set; }
}
