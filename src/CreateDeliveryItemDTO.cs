using System;

namespace DeliveryService.DTO;
{
	public class CreateDeliveryItemDTO
	{
    public required Guid DeliveryGuid { get; set; }
   
    [Range(0, double.MaxValue)]
    public required decimal TotalPrice { get; set; }
    public required decimal Total_Quantity { get; set; }
        
    }
}

 


