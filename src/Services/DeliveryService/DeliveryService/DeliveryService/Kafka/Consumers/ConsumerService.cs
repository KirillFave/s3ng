using System.Text.Json;
using Azure.Core;
using Confluent.Kafka;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;
using Mailchimp.Core;
using DeliveryService.Domain.External.Entities;

namespace DeliveryService.Kafka.Consumers
{
    public class ConsumerService : BackgroundService 
    {
        private readonly IConsumer<Null, string> _kafkaConsumer;
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public ConsumerService(IConsumer<Null, string> kafkaConsumer, IOrderRepository orderRepository, IDeliveryRepository deliveryRepository)
        {
            _kafkaConsumer = kafkaConsumer;
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _kafkaConsumer.Subscribe("order-events");
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = _kafkaConsumer.Consume(stoppingToken);
                var order = JsonSerializer.Deserialize<Order>(consumeResult.Message.Value);
                Console.WriteLine("Order received in OrderConsumerService:\nData: {consumeResult.Message.Value}");
                Update delivery status in the database.
                await UpdateOrderStatusAsync(order);
            }
        }

        private async Task UpdateOrderStatusAsync(Order order)
        {
            //await _orderRepository.UpdateStatusOrder(order);
            //await _deliveryRepository.SaveDeliveryStatus(order);

            //var orderObj = _orderContext.Orders.Find(order.Id);
            //if (orderObj == null) return Task.CompletedTask;
            //orderObj.Status = order.Status;
            //_orderContext.Orders.Update(orderObj);
            //return _orderContext.SaveChangesAsync();
        }
    }
}
