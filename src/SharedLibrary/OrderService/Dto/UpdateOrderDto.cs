using SharedLibrary.OrderService.Models;

using System.ComponentModel.DataAnnotations;

namespace SharedLibrary.OrderService.Dto;

public class UpdateOrderDto
{
    public required Guid Id { get; set; }
    public PaymentType? PaymentType { get; set; }

    [MaxLength(255)]
    public string? ShipAddress { get; set; }
}
