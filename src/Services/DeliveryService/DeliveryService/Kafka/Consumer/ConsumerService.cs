using Azure.Core;
using Confluent.Kafka;
using Mailchimp.Core;

namespace DeliveryService.Kafka.Consumer
{
    public class ConsumerService : BackgroundService
    {
        private readonly IConfiguration _config;
        //private readonly IElasticLogger _logger;
        private readonly ConsumerConfig _consumerConfig;
        private readonly string[] _topics;
        private readonly double _maxNumAttempts;
        private readonly double _retryIntervalInSec;

        public ConsumerService(IConfiguration config)
        {
            _config = config;            
            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _config.GetValue<string>("Kafka:BootstrapServers"),
                GroupId = _config.GetValue<string>("Kafka:GroupId"),
                EnableAutoCommit = _config.GetValue<bool>("Kafka:Consumer:EnableAutoCommit"),
                AutoOffsetReset = (AutoOffsetReset)_config.GetValue<int>("Kafka:Consumer:AutoOffsetReset")
            };
            _topics = _config.GetValue<string>("Kafka:Consumer:Topics").Split(',');
            _maxNumAttempts = _config.GetValue<double>("App:MaxNumAttempts");
            _retryIntervalInSec = _config.GetValue<double>("App:RetryIntervalInSec");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("!!! CONSUMER STARTED !!!\n");

            // Starting a new Task here because Consume() method is synchronous
            var task = Task.Run(() => ProcessQueue(stoppingToken), stoppingToken);

            return task;
        }

        private void ProcessQueue(CancellationToken stoppingToken)
        {
            using (var consumer = new ConsumerBuilder<Ignore, Request>(_consumerConfig)
            .Build()) 
            {
                consumer.Subscribe(_topics);

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(stoppingToken);

                            // Don't want to block consume loop, so starting new Task for each message  
                            Task.Run(async () =>
                            {
                                var currentNumAttempts = 0;
                                var committed = false;

                                //var response = new Response();

                                //while (currentNumAttempts < _maxNumAttempts)
                                //{
                                //    currentNumAttempts++;

                                //    // SendDataAsync is a method that sends http request to some end-points
                                //    response = await Helper.SendDataAsync(consumeResult.Value, _config, _logger);

                                //    //if (response != null && response.Code >= 0)
                                //        if (response != null)
                                //        {
                                //        try
                                //        {
                                //            consumer.Commit(consumeResult);
                                //            committed = true;

                                //            break;
                                //        }
                                //        catch (KafkaException ex)
                                //        {
                                //            // log
                                //        }
                                //    }
                                //    else
                                //    {
                                //        // log
                                //    }

                                //    if (currentNumAttempts < _maxNumAttempts)
                                //    {
                                //        // Delay between tries
                                //        await Task.Delay(TimeSpan.FromSeconds(_retryIntervalInSec));
                                //    }
                                //}

                                if (!committed)
                                {
                                    try
                                    {
                                        consumer.Commit(consumeResult);
                                    }
                                    catch (KafkaException ex)
                                    {
                                        // log
                                    }
                                }
                            }, stoppingToken);
                        }
                        catch (ConsumeException ex)
                        {
                            // log
                        }
                    }
                }
                catch (OperationCanceledException ex)
                {
                    // log
                    consumer.Close();
                }
            }
        }
    }
}
