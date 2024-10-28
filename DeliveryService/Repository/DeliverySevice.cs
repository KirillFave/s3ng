using DeliveryService.Data;
using DeliveryService.Models;

namespace DeliveryService.Repository
{
    public class DeliverySevice : IDeliveryService
    {
        DataContext _context;
        public DeliverySevice(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Delivery> GetDeliveries()
        {
            return _context.Deliveries.ToList();
        }
        public Delivery DeliveryService(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            _context.SaveChangesAsync();
            return delivery;
        }

       
    }
}
