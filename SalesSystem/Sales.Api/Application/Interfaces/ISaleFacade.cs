using Sales.Api.Application.DTOs;
namespace Sales.Api.Application.Interfaces
{
    public interface ISaleFacade
    {
        Task<int> RegisterSaleAsync(CreateSaleDto dto);
    }
}
