using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors.Infrastructure;
using DeliveryService.Services;

namespace DeliveryService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DeliveryServiceController : ControllerBase
    {
        private readonly IDeliveryService _service;
        private readonly ILogger<DeliveryServiceController> _logger;

        public DeliveryServiceController(IDeliveryService service, ILogger<DeliveryServiceController> logger)
        {
            _service = service;           
            _logger = logger;
            
        }

    }
}
