using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Api.Application.DTOs;
using Sales.Api.Application.Interfaces;

namespace Sales.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISaleFacade _facade;
        private readonly ISaleRepository _repository;

        public SaleController(ISaleFacade facade, ISaleRepository repository)
        {
            _facade = facade;
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateSaleDto dto)
        {
            try
            {
                var saleId = await _facade.RegisterSaleAsync(dto);

                return Ok(new
                {
                    Success = true,
                    Message = "Venta registrada correctamente",
                    IdVenta = saleId
                });

            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }           
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var sale = await _repository.GetSalesAsync();           
            return Ok(sale);
        }
    }
}
