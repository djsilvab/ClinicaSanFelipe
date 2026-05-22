using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Api.Application.Interfaces;

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KardexController : ControllerBase
    {
        private readonly ISaleRepository _repository;

        public KardexController(ISaleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var kardex = await _repository.GetKardexAsync();
            return Ok(kardex);
        }
    }
}
