using Sales.Api.Application.DTOs;
using Sales.Api.Application.Interfaces;
using Sales.Api.Domain.Entities;
using Sales.Api.Infrastructure.Persistence;

namespace Sales.Api.Application.Facades
{
    public class SaleFacade : ISaleFacade
    {
        private readonly ISaleRepository _repository;
        private readonly SalesDbContext _context;

        public SaleFacade(ISaleRepository repository, SalesDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<int> RegisterSaleAsync(CreateSaleDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                foreach (var item in dto.Details)
                {
                    var stock = await _repository.GetCurrentStockAsync(item.Id_producto);
                    if (stock < item.Cantidad)
                    {
                        throw new Exception($"Stock insuficiente del producto {item.Id_producto}. Disponible: {stock}");
                    }
                }
                decimal subTotal = dto.Details.Sum(x => x.Cantidad * x.Precio);
                decimal igv = subTotal * 0.18m;
                decimal total = subTotal + igv;
                var sale = new SaleCab
                {
                    fecRegistro = DateTime.UtcNow,
                    SubTotal = subTotal,
                    Igv = igv,
                    Total = total,
                    Details = dto.Details.Select(d => new SaleDet
                    {
                        Id_producto = d.Id_producto,
                        Cantidad = d.Cantidad,
                        Precio = d.Precio,
                        Sub_Total = d.Cantidad * d.Precio,
                        Igv = d.Cantidad * d.Precio * 0.18m,
                        Total = (d.Cantidad * d.Precio) * 1.18m
                    }).ToList()
                };
                sale = await _repository.AddSaleAsync(sale);
                var movement = new MovementCab
                {
                    Fec_registro = DateTime.UtcNow,
                    Id_TipoMovimiento = 2,
                    Id_DocumentoOrigen = sale.Id_VentaCab,
                    Details = dto.Details.Select(d => new MovementDet
                    {
                        Id_Producto = d.Id_producto,
                        Cantidad = d.Cantidad
                    }).ToList()
                };
                await _repository.AddMovementAsync(movement);
                await transaction.CommitAsync();
                return sale.Id_VentaCab;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
