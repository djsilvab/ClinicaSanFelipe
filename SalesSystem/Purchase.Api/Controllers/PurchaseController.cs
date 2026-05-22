using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Purchase.Api.Application.DTOs;
using Purchase.Api.Application.Interfaces;
namespace Purchase.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseFacade _facade;
    private readonly IPurchaseRepository _repository;

    public PurchaseController(IPurchaseFacade facade, IPurchaseRepository repository)
    {
        _facade = facade;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Register(CreatePurchaseDto dto)
    {
        var id = await _facade.RegisterPurchaseAsync(dto);
        return Ok(new
        {
            IdCompra = id
        });
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        var purchases = await _repository.GetPurchasesAsync();
        return Ok(purchases);
    }
}