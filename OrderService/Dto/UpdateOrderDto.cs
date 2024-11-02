using OrderService.Models;

using System.ComponentModel.DataAnnotations;

namespace OrderService.Dto;

public class UpdateOrderDto
{
    public required Guid Guid { get; set; }
    public PaymentType? PaymentType { get; set; }

    [MaxLength(255)]
    public string? ShipAddress { get; set; }
}
