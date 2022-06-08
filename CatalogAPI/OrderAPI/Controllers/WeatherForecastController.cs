using Microsoft.AspNetCore.Mvc;

namespace OrderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        { Random random = new Random();
            var randomValue=random.Next(0, Summaries.Length);
            _logger.LogInformation($"Random Value is {randomValue}");
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string ThrowErrorMessage(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception($"id cannot be less than or equal to o. value passed is {id}");
                return id.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return string.Empty;
        }
    }
}