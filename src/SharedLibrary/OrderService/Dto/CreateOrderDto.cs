using SharedLibrary.OrderService.Models;

namespace SharedLibrary.OrderService.Dto;

public class CreateOrderDto
{
    public Guid UserGuid { get; set; }
    public required List<CreateOrderItemDto> OrderItems { get; set; }
    public OrderStatus Status { get; set; }
    public PaymentType PaymentType { get; set; }
    public string ShipAddress { get; set; }
}
