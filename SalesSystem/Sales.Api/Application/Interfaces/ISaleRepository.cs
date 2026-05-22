using Sales.Api.Application.DTOs;
using Sales.Api.Domain.Entities;
namespace Sales.Api.Application.Interfaces
{
    public interface ISaleRepository
    {
        Task<SaleCab> AddSaleAsync(SaleCab sale);

        Task AddMovementAsync(MovementCab movement);

        Task<List<SaleCab>> GetSalesAsync();

        Task<int> GetCurrentStockAsync(int productId);

        Task<List<KardexDto>> GetKardexAsync();
    }
}
