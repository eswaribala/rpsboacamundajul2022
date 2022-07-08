using Camunda.Worker;
using Confluent.Kafka;
using loanprocessapi.Models;

using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace loanprocessapi.Handlers
{
    [HandlerTopics("kafkamessage")]
    public class KafkaProcessHandler : IExternalTaskHandler
    {
        private ILogger<KafkaProcessHandler> _logger;
        private IConfiguration _configuration;
        public KafkaProcessHandler(IConfiguration configuration,ILogger<KafkaProcessHandler> logger)
        {
            _configuration = configuration;
            _logger = logger;


        }

        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            //read the values from camunda process
          
           string orderId = externalTask.Variables["OrderId"].Value.ToString();
           string orderAmount = externalTask.Variables["OrderAmount"].Value.ToString();
           string orderDate = externalTask.Variables["OrderDate"].Value.ToString();
            await Task.Delay(1000);
            this._logger.LogInformation($"Order Id: {orderId}");
            Order order = new Order
            {
                OrderId = Convert.ToInt64(orderId),
                OrderAmount=Convert.ToInt64(orderAmount),
                OrderDate=orderDate,
             
            };
            //convert to json
            string message = JsonSerializer.Serialize(order);

            //Kafka server access
            ProducerConfig Config = new ProducerConfig
            {
                BootstrapServers = _configuration["BootStrapServer"],

                ClientId = Dns.GetHostName()

            };
            string topicName = _configuration["TopicName"];
            //publish the message
            try
            {
                using (var producer = new ProducerBuilder
                <Null, string>(Config).Build())
                {
                    var result = await producer.ProduceAsync
                    (topicName, new Message<Null, string>
                    {
                        Value = message
                    });

                    _logger.LogInformation($"Delivery Timestamp:{ result.Timestamp.UtcDateTime}");
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

           

            return new CompleteResult();
            
        }
    }
}
