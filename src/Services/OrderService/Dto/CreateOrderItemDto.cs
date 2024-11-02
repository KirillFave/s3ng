using System.ComponentModel.DataAnnotations;

namespace OrderService.Dto;

public class CreateOrderItemDto
{
    public required Guid OrderGuid { get; set; }
    public required Guid ProductGuid { get; set; }

    [Range(0, double.MaxValue)]
    public required decimal PricePerUnit { get; set; }

    [Range(1, 100)]
    public required int Count { get; set; }
}
