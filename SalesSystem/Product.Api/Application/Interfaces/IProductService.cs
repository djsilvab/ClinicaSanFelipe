using Product.Api.Application.DTOs;
using Product.Api.Domain.Entities;

namespace Product.Api.Application.Interfaces;

public interface IProductService
{
    Task<Product.Api.Domain.Entities.Product> CreateAsync(
        CreateProductDto dto);

    Task<List<Product.Api.Domain.Entities.Product>>
        GetAllAsync();

    Task<Product.Api.Domain.Entities.Product?>
        UpdateAsync(
            int id,
            UpdateProductDto dto);

    Task<bool>
        DeleteAsync(int id);


}
