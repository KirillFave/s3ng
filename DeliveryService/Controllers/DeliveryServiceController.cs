using Microsoft.AspNetCore.Mvc;
using DeliveryService.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;



namespace DeliveryService.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class DeliveryServiceController : ControllerBase
    //{

    //    Repository.IDeliveryService _deliveryService;

    //    public DeliveryServiceController(Repository.IDeliveryService deliveryService)
    //    {
    //        _deliveryService = deliveryService;
    //    }



    //    public IEnumerable<Delivery> GetDeliveries() => _deliveryService.GetDeliveries();

    //    public Delivery DeliveryService(Delivery delivery)
    //    {
    //        return _deliveryService.DeliveryService(delivery);
    //    }

    //}
    [Route("api/[controller]")]
    [ApiController]

    public class DeliveryServiceController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Delivery>>> GetAllDeliveries()
        {
            var deliveries = new List<Delivery>
            {
                new Delivery
                {
                    Id = new Guid(),
                    OrderStatus = "active",
                    Total_Price = 1.111M,
                    Shipping_Address = "1st Avenue bld.13",
                    //PaymentType = "cash",
                    Order_Id = 111,
                    Courer_Id = 111,
                    Delivery_Time = DateTime.Now
                }
            };

            return Ok(deliveries);
        }

    }
}
