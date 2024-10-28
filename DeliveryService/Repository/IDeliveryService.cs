
using DeliveryService.Models;

namespace DeliveryService.Repository
{
    public interface IDeliveryService
    {
        public IEnumerable<Delivery> GetDeliveries();
        public Delivery DeliveryService(Delivery delivery); 
    }
}
