using Camunda.Worker;

namespace loanprocessapi.Handlers
{
    [HandlerTopics("kafkamessage")]
    public class KafkaProcessHandler : IExternalTaskHandler
    {
        private ILogger<LogHandler> _logger;

        public KafkaProcessHandler(ILogger<LogHandler> logger)
        {
            _logger = logger;
        }
        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            //read the values from camunda process
          
            string orderId = externalTask.Variables["OrderId"].Value.ToString();
            
            await Task.Delay(1000);
            this._logger.LogInformation($"Order Id: {orderId}");

            return new CompleteResult();
            
        }
    }
}
