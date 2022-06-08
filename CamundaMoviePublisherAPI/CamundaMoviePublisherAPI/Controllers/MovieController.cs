using Camunda.Api.Client;
using Camunda.Api.Client.ProcessDefinition;
using CamundaMoviePublisherAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CamundaMoviePublisherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private IConfiguration _configuration;
        private CamundaClient _client;
        public MovieController(ILogger<MovieController> logger, IConfiguration configuration)
        {
            this._configuration = configuration;
            this._logger = logger;
            _client = CamundaClient.Create(this._configuration["RestApiUri"]);
        }
        [HttpPost("startProcess")]
        public IActionResult StartProcess([FromBody] Movie Movie, 
            MovieBPMNProcess MovieBPMNProcess)
        {
            _logger.LogInformation("Starting the sample Camunda process...");
            try
            {
                Random random = new Random();

                //Creating process parameters
                StartProcessInstance processParams;

                //json to string
                String message = JsonConvert.SerializeObject(Movie);
                //string to c# pobject
                Movie MovieObj= JsonConvert.DeserializeObject<Movie>(message);



                processParams = new StartProcessInstance()
                    .SetVariable("MovieId", Movie.MovieId)
                   .SetVariable("DirectorName", Movie.DirectorName)
                   .SetVariable("Title", Movie.Title)
                   .SetVariable("ReleaseDate", Movie.ReleasedDate);

                _logger.LogInformation($"Camunda process to demonstrate Saga based orchestrator started..........");


                //Startinng the process
                var proceStartResult = _client.ProcessDefinitions.ByKey(MovieBPMNProcess.ToString())
                    .StartProcessInstance(processParams);

                //return Ok("Done");

                return Ok(proceStartResult.Result.DefinitionId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"error occured!! error messge: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("processDefinitions")]
        //camunda rest api calls
        public async Task<IActionResult> GetProcessDefinitions()
        {



            using var client = new HttpClient();

            var url = this._configuration["RestApiUri"] + "process-definition";
            //synchronous call
            var result = await client.GetAsync(url);
            return Ok(result.Content.ReadAsStringAsync());
            //return Ok(this._client.ProcessDefinitions);
        }
       

        public enum MovieBPMNProcess
        {
            Process_Movie
        }

    }
}
