using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracingController : ControllerBase
    {

        private readonly ILogger<TracingController> _logger;
        private ITracer _tracer;

        public TracingController(ILogger<TracingController> logger, ITracer tracer)
        {
            _logger = logger;
            _tracer = tracer;
        }


        [HttpGet]
        public string Get()
        {
            var rng = new Random();
            var actionName = ControllerContext.ActionDescriptor.DisplayName;
            using var scope = _tracer.BuildSpan(actionName).StartActive(true);
            scope.Span.Log("Request added to Tracer....");
            return rng.Next(1, 10).ToString();
        }


    }
}
