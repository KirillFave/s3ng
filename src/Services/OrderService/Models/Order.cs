namespace OrderService.Models;

public class Order : BaseEntity
{
    public Guid UserId { get; }
    public List<OrderItem> Items { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentType PaymentType { get; set; }
    public string ShipAddress { get; set; }
    public DateTime CreatedTimestamp { get; set; }

    public Order() : base()
    {

    }
}
