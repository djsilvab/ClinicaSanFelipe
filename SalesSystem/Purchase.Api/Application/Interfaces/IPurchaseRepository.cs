using Purchase.Api.Domain.Entities;

namespace Purchase.Api.Application.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<PurchaseCab> AddPurchaseAsync(PurchaseCab purchase);

        Task AddMovementAsync(MovementCab movement);

        Task<List<PurchaseCab>> GetPurchasesAsync();

    }
}
