using CatalogAPI.Auth;
using CatalogAPI.Models;
using CatalogAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogAPI.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private ICatalogRepository _CatalogRepository;
        private IConfiguration _configuration;

        public CatalogController(ICatalogRepository
            catalogRepository, IConfiguration configuration
            )
        {
            _CatalogRepository = catalogRepository;
            _configuration = configuration;
        }

        // GET: api/<CatalogController>
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<IEnumerable<Catalog>> Get()
        {
            return await this._CatalogRepository.GetAllCatalog();
        }

       


        // GET api/<CatalogController>/5
        [HttpGet("{CatalogId}")]
        public async Task<Catalog> Get(long CatalogId)
        {
            return await this._CatalogRepository.GetCatalogById(CatalogId);
        }

        // POST api/<CatalogController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Catalog Catalog)
        {
            await this._CatalogRepository.AddCatalog(Catalog);
            return CreatedAtAction(nameof(Get),
                            new { id =Catalog.CatalogId }, Catalog);

        }

        // PUT api/<CatalogController>/5
        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] Catalog Catalog)
        {
            await this._CatalogRepository.UpdateCatalog(Catalog);
            return CreatedAtAction(nameof(Get),
                            new { id = Catalog.CatalogId }, Catalog);
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (await this._CatalogRepository.DeleteCatalog(id))
                return new OkResult();
            else
                return new BadRequestResult();

        }
    }
}
