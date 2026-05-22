namespace Product.Api.Application.Interfaces;

using Product.Api.Domain.Entities;

public interface IProductRepository
{
    Task<Product> CreateAsync(
        Product product);

    Task<List<Product>>
        GetAllAsync();

    Task<Product?>
        GetByIdAsync(int id);

    Task UpdateAsync(
        Product product);

    Task DeleteAsync(
        Product product);
}
