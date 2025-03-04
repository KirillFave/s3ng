using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using DeliveryService.Delivery.BusinessLogic.Services.Delivery.Repositories;
using DeliveryService.Domain.External.Entities;
using DeliveryService.Kafka.Models;
using DeliveryService.Kafka.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DeliveryService.Kafka.Consumers;

public abstract class ConsumerBackgroundService<TKey, TValue> : BackgroundService where TValue : IKafkaMessage
{
    private ILogger _logger;
    private BaseConsumer<TKey, TValue> _baseConsumer;
    private ApplicationOptions _applicationOptions;

    protected abstract string TopicName { get; }
    private readonly IOrderRepository _orderRepository;
    private readonly IDeliveryRepository _deliveryRepository;

    public ConsumerBackgroundService(
        BaseConsumer<TKey, TValue> _baseConsumer,
        ILogger logger,
        ApplicationOptions applicationOptions,
        IOrderRepository orderRepository,
        IDeliveryRepository deliveryRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
        _deliveryRepository = deliveryRepository;

        _baseConsumer = new BaseConsumer<TKey, TValue>(applicationOptions.KafkaOptions, logger, applicationOptions.GroupId);
        _logger = logger;

    }

    protected ConsumerBackgroundService(ILogger<NewOrderConsumer> logger, ApplicationOptions applicationOptions)
    {
        _logger = logger;
        _applicationOptions = applicationOptions;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _baseConsumer.Consumer.Subscribe(TopicName);
        _logger.LogInformation($"Start consumer topic {TopicName}");
        while (!stoppingToken.IsCancellationRequested)
        {
            await Consume(stoppingToken);
            //var consumeResult = _baseConsumer.Consumer.Consume(stoppingToken);
            //var order = JsonSerializer.Deserialize<Order>(consumeResult.Message.Value);
            //Console.WriteLine("Order received in OrderConsumerService:\nData: {consumeResult.Message.Value}");
            //// Update delivery status in the database.
            //await UpdateOrderStatusAsync(order);
        }

        _baseConsumer.Consumer.Unsubscribe();
        _logger.LogInformation($"Stop consumer topic {TopicName}");
    }

    private async Task Consume(CancellationToken cancellationToken)
    {
        ConsumeResult<TKey, TValue> message = null;
        try
        {
            message = _baseConsumer.Consumer.Consume(TimeSpan.FromSeconds(10)); //можно подставлять cancellationToken но тогда под капотом он подставит свое прерывание 5 сек и непредсказуемое поведение

            if (message is null)
            {
                await Task.Delay(100, cancellationToken);
                return;
            }

            await HandleAsync(message, cancellationToken);
            _baseConsumer.Consumer.Commit();
        }
        catch (Exception e)
        {
            var key = message is null ? message.Message.Key.ToString() : "No key";
            var value = message is null ? message.Message.Value.ToString() : "No value";
            _logger.LogError(e, $"Error process message with key {key}, value {value}");
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

    protected abstract Task HandleAsync(ConsumeResult<TKey, TValue> message, CancellationToken cancellationToken);

    public override void Dispose()
    {
        base.Dispose();
    }
}
