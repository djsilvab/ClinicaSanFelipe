using Purchase.Api.Application.DTOs;
using Purchase.Api.Application.Interfaces;
using Purchase.Api.Domain.Entities;
using Purchase.Api.Infrastructure.Persistence;

namespace Purchase.Api.Application.Facades
{
    public class PurchaseFacade : IPurchaseFacade
    {
        private readonly IPurchaseRepository _repository;
        private readonly PurchaseDbContext _context;

        public PurchaseFacade(IPurchaseRepository repository,
                              PurchaseDbContext context
            )
        {
            _repository = repository;
            _context = context;
        }

        public async Task<int> RegisterPurchaseAsync(CreatePurchaseDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                decimal subTotal = dto.Details.Sum(x => x.Cantidad * x.Precio);

                decimal igv = subTotal * 0.18m;

                decimal total = subTotal + igv;

                var purchase = new PurchaseCab {
                        FecRegistro = DateTime.UtcNow,
                        SubTotal = subTotal,
                        Igv = igv,
                        Total = total,
                        Details = dto.Details.Select(d =>
                                    new PurchaseDet
                                    {
                                        Id_producto = d.Id_producto,
                                        Cantidad = d.Cantidad,
                                        Precio = d.Precio,
                                        Sub_Total = d.Cantidad * d.Precio,
                                        Igv = d.Cantidad * d.Precio * 0.18m,
                                        Total = (d.Cantidad * d.Precio) * 1.18m
                                    }).ToList()
                    };

                purchase = await _repository.AddPurchaseAsync(purchase);

                var movement = new MovementCab{
                                    Fec_registro =DateTime.UtcNow,
                                    Id_TipoMovimiento = 1,
                                    Id_DocumentoOrigen = purchase.Id_CompraCab,
                                    Details = dto.Details
                                                .Select(d =>
                                                    new MovementDet
                                                    {
                                                        Id_Producto = d.Id_producto,
                                                        Cantidad = d.Cantidad
                                                    })
                                                .ToList()
                    };

                await _repository.AddMovementAsync(movement);

                await transaction.CommitAsync();

                return purchase.Id_CompraCab;
            }
            catch
            {
                await transaction.RollbackAsync();

                throw;
            }
        }

    }
}
