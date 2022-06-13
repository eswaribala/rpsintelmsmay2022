using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Services;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollyController : ControllerBase
    {

        private readonly IPollyInvoker _pollyInvoker;

        public PollyController(IPollyInvoker pollyInvoker)
        {
           _pollyInvoker = pollyInvoker;

        }
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _pollyInvoker.EnsureCancellation(cancellationToken);
            return Ok(result);
        }
    }
}
