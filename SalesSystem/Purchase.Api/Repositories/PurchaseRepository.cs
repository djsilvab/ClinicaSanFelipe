using Microsoft.EntityFrameworkCore;
using Purchase.Api.Application.Interfaces;
using Purchase.Api.Domain.Entities;
using Purchase.Api.Infrastructure.Persistence;

namespace Purchase.Api.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly PurchaseDbContext _context;

        public PurchaseRepository(PurchaseDbContext context)
        {
            _context = context;
        }

        public async Task<PurchaseCab> AddPurchaseAsync(PurchaseCab purchase)
        {
            _context.PurchaseCab.Add(purchase);

            await _context.SaveChangesAsync();

            return purchase;
        }

        public async Task AddMovementAsync(MovementCab movement)
        {
            _context.MovementCab.Add(movement);

            await _context.SaveChangesAsync();
        }

        public async Task<List<PurchaseCab>> GetPurchasesAsync()
        {
            return await _context.PurchaseCab.Include(x => x.Details)
                    .ToListAsync();
        }
    }
}
