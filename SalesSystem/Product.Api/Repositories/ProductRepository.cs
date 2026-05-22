using Microsoft.EntityFrameworkCore;
using Product.Api.Application.Interfaces;
using Product.Api.Infrastructure.Persistence;

namespace Product.Api.Repositories;

public class ProductRepository
    : IProductRepository
{
    private readonly ProductDbContext
        _context;

    public ProductRepository(
        ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Product.Api.Domain.Entities.Product>
        CreateAsync(Product.Api.Domain.Entities.Product product)
    {
        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<List<Product.Api.Domain.Entities.Product>>
        GetAllAsync()
    {
        return await _context.Products
            .ToListAsync();
    }

    public async Task<Product.Api.Domain.Entities.Product?>
        GetByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(
                x => x.Id_producto == id);
    }

    public async Task UpdateAsync(
        Product.Api.Domain.Entities.Product product)
    {
        _context.Products.Update(product);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(
        Product.Api.Domain.Entities.Product product)
    {
        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
    }
}
