using Microsoft.EntityFrameworkCore;
using Sales.Api.Application.Interfaces;
using Sales.Api.Domain.Entities;
using Sales.Api.Infrastructure.Persistence;
using Sales.Api.Application.DTOs;

namespace Sales.Api.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly SalesDbContext _context;

        public SaleRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task AddMovementAsync(MovementCab movement)
        {
            _context.MovementCab.Add(movement);
            await _context.SaveChangesAsync();
        }

        public async Task<SaleCab> AddSaleAsync(SaleCab sale)
        {
            _context.SaleCab.Add(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<int> GetCurrentStockAsync(int productId)
        {
            var entradas = await _context
                                    .MovementDet
                                    .Where(x => x.Id_Producto == productId && x.MovementCab!.Id_TipoMovimiento == 1)
                                    .SumAsync(x => (int?)x.Cantidad)?? 0;

            var salidas = await _context.MovementDet.Where(x =>
                                    x.Id_Producto == productId && x.MovementCab!.Id_TipoMovimiento == 2)
                                    .SumAsync(x => (int?)x.Cantidad)?? 0;

            return entradas - salidas;
        }       

        public async Task<List<SaleCab>> GetSalesAsync()
        {
            return await _context.SaleCab.Include(x => x.Details).ToListAsync();
        }

        public async Task<List<KardexDto>> GetKardexAsync()
        {
            var movimientos = await _context.MovementDet
                                    .Include(x => x.MovementCab)
                                    .OrderBy(x => x.MovementCab!.Fec_registro)
                                    .ToListAsync();

            var result = new List<KardexDto>();

            var stockPorProducto = new Dictionary<int, int>();

            foreach (var item in movimientos)
            {
                int signo = item.MovementCab!.Id_TipoMovimiento == 1 ? 1 : -1;

                if (!stockPorProducto.ContainsKey(item.Id_Producto))
                {
                    stockPorProducto[item.Id_Producto] = 0;
                }

                stockPorProducto[item.Id_Producto] += signo * item.Cantidad;

                result.Add(new KardexDto
                {
                    ProductoId = item.Id_Producto,
                    Fecha = item.MovementCab.Fec_registro,
                    TipoMovimiento = item.MovementCab.Id_TipoMovimiento == 1 ? "Entrada" : "Salida",
                    Cantidad = item.Cantidad,
                    DocumentoOrigen = item.MovementCab.Id_DocumentoOrigen,
                    StockActual = stockPorProducto[item.Id_Producto]
                });
            }
           
            return result;
        }
    }
}
