using Purchase.Api.Application.DTOs;

namespace Purchase.Api.Application.Interfaces
{
    public interface IPurchaseFacade
    {
        Task<int> RegisterPurchaseAsync(CreatePurchaseDto dto);
    }
}
