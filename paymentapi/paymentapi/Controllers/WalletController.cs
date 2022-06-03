﻿using Camunda.Api.Client;
using Camunda.Api.Client.ProcessDefinition;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using paymentapi.Models;
using System.Net.Http.Headers;

namespace paymentapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ILogger<WalletController> _logger;
        private IConfiguration _configuration;
        private CamundaClient _client;
        public WalletController(ILogger<WalletController> logger,IConfiguration configuration)
        {
            this._configuration = configuration;
            this._logger = logger;
            _client = CamundaClient.Create(this._configuration["RestApiUri"]);
        }
        [HttpPost("startProcess")]
        public IActionResult StartProcess([FromBody] Wallet wallet,MyBPMNProcess myBPMNProcess)
        {
            _logger.LogInformation("Starting the sample Camunda process...");
            try
            {
                Random random = new Random();

                //Creating process parameters
                StartProcessInstance processParams;

                //json to string
                String message = JsonConvert.SerializeObject(wallet);
                //string to c# pobject
                Wallet Wallet =JsonConvert.DeserializeObject<Wallet>(message);



                processParams = new StartProcessInstance()

                   .SetVariable("walletId", Wallet.WalletId)
                   .SetVariable("walletBalance", Wallet.WalletBalance);                 

                _logger.LogInformation($"Camunda process to demonstrate Saga based orchestrator started..........");


                //Startinng the process
                var proceStartResult = _client.ProcessDefinitions.ByKey(myBPMNProcess.ToString())
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
        [HttpPost("tasks")]
        public async Task<List<TaskList>> GetTasksList()
        {
            using (var client = new HttpClient()) {

                var url = this._configuration["RestApiUri"] + "task";
                var response = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<TaskList>>(response);
            }
               
            
        }

    }
    public enum MyBPMNProcess
    {
        Process_Wallet
    }
}
