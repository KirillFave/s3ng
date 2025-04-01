namespace SharedLibrary.OrderService.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserGuid { get; set; }
    public virtual ICollection<OrderItem> Items { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentType PaymentType { get; set; }
    public string ShipAddress { get; set; }
    public DateTime CreatedTimestamp { get; set; }
    public bool IsCanceled { get; set; }
}
