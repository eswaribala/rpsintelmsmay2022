using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviePublisher.Models;
using MoviePublisher.Services;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace MoviePublisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageService _messageService;

        public MovieController(IConfiguration configuration,IMessageService messageService)
        {
            _configuration = configuration;
            _messageService = messageService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie Movie)
        {
            string topicName = this._configuration["TopicName"];
            string message = JsonSerializer.Serialize(Movie);
            return Ok(await _messageService.PublishMovie(topicName, message,_configuration));

        }

       




    }
}
