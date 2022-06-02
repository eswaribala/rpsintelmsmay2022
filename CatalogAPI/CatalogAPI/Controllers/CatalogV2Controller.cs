
using CatalogAPI.Models;
using CatalogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogAPI.Controllers
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CatalogV2Controller : ControllerBase
    {
        private ICatalogRepository _CatalogRepository;
        private IConfiguration _configuration;
        public CatalogV2Controller(ICatalogRepository catalogRepository,
            IConfiguration configuration)
        {
            _CatalogRepository = catalogRepository;
            _configuration = configuration;
        }

        // GET api/values
        [HttpGet]

        public ActionResult<IEnumerable<string>> Get()
        {
            var val1 = _configuration["ipaddress"];
            var val2 = _configuration["port"];
            return new string[] { val1, val2 };
        }
    }
}
