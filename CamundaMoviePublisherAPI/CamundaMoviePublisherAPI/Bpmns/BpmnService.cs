using Camunda.Api.Client;
using Camunda.Api.Client.Deployment;
using Camunda.Api.Client.ProcessInstance;

namespace CamundaMoviePublisherAPI.Bpmns
{
    public class BpmnService
    {
        private readonly CamundaClient camunda;
        public BpmnService(String RestApiService)
        {
            camunda = CamundaClient.Create(RestApiService);
        }
        public async Task DeployProcessDefinition()
        {
            var bpmnResourceStream = this.GetType()
                .Assembly
                .GetManifestResourceStream("CamundaMoviePublisherAPI.Processes.movie.bpmn");

            try
            {
                await camunda.Deployments.Create(
                    "Movie Deployment",
                    true,
                    true,
                    null,
                    null,
                    new ResourceDataContent(bpmnResourceStream, "movie.bpmn"));
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to deploy process definition", e);
            }
        }

        public async Task CleanupProcessInstances()
        {
            var instances = await camunda.ProcessInstances
                .Query(new ProcessInstanceQuery
                {
                    ProcessDefinitionKey = "Process_"
                })
                .List();

            if (instances.Count > 0)
            {
                await camunda.ProcessInstances.Delete(new DeleteProcessInstances
                {
                    ProcessInstanceIds = instances.Select(i => i.Id).ToList()
                });
            }
        }
    }
}
