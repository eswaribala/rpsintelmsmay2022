using Camunda.Worker;
using Confluent.Kafka;
using System.Net;

namespace CamundaMoviePublisherAPI.Handlers
{
    [HandlerTopics("kafkahandler")]
    public class KafkaTaskHandler : IExternalTaskHandler
    {
        private readonly ILogger<KafkaTaskHandler> _logger;
        private readonly IConfiguration _configuration;
        
        public KafkaTaskHandler(ILogger<KafkaTaskHandler> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Movie Data handler is called from Camunda...");
            try
            {
                _logger.LogInformation($"Movie Data processing..........");
                //Mimicking operation
                Task.Delay(500).Wait();

                _logger.LogInformation($"Movie Data processing, {externalTask.Variables["DirectorName"].Value} ");

                var topicName = this._configuration["TopicName"];
                string message = externalTask.Variables["DirectorName"].Value.ToString();
                await ProcessMovie(topicName, message);

                return new CompleteResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"error occured!! error messge: {ex.Message}");
                //return failure
                return new BpmnErrorResult("Movie Data Processing Failure",
                    "Error occured while processing payment..");
            }
        }


        private async Task<string> ProcessMovie(string topicName, string message)
        {
            ProducerConfig Config = new ProducerConfig
            {
                BootstrapServers = this._configuration["BootStrapServer"],

                ClientId = Dns.GetHostName()

            };
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

                    this._logger.LogInformation($"Delivery Timestamp:{ result.Timestamp.UtcDateTime}");
                    return await Task.FromResult($"Delivery Timestamp:{ result.Timestamp.UtcDateTime}");
                }
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"Error occured: {ex.Message}");
            }

            return await Task.FromResult("Not Published.....");
        }
    }
}
