using Camunda.Worker;

namespace CamundaMoviePublisherAPI.Handlers
{
    [HandlerTopics("movietask")]
    public class MovieTaskHandler : IExternalTaskHandler
    {
        public Task<IExecutionResult> HandleAsync(ExternalTask externalTask, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
