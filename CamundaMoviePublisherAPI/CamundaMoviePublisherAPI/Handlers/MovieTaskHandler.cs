using Camunda.Worker;

namespace CamundaMoviePublisherAPI.Handlers
{
    [HandlerTopics("movietask")]
    public class MovieTaskHandler : IExternalTaskHandler
    {
        private readonly ILogger<MovieTaskHandler> _logger;
        public MovieTaskHandler(ILogger<MovieTaskHandler> logger)
        {
            _logger = logger;
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
    }
}
