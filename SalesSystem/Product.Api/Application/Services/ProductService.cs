using Product.Api.Application.DTOs;
using Product.Api.Application.Interfaces;

namespace Product.Api.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Product> CreateAsync(CreateProductDto dto)
    {
        var product =
            new Domain.Entities.Product
            {
                Nombre_producto =
                    dto.Nombre_producto,

                NroLote =
                    dto.NroLote,

                Fec_registro =
                    DateTime.UtcNow,

                Costo =
                    dto.Costo,

                PrecioVenta =
                    dto.PrecioVenta
            };

        return await _repository
            .CreateAsync(product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product =
            await _repository
            .GetByIdAsync(id);

        if (product == null)
        {
            return false;
        }

        await _repository
            .DeleteAsync(product);

        return true;
    }

    public async Task<List<Domain.Entities.Product>> GetAllAsync()
    {
        return await _repository
            .GetAllAsync();
    }

    public async Task<Domain.Entities.Product?> UpdateAsync(int id, UpdateProductDto dto)
    {
        var product =
            await _repository
            .GetByIdAsync(id);

        if (product == null)
        {
            return null;
        }

        product.Nombre_producto =
            dto.Nombre_producto;

        product.NroLote =
            dto.NroLote;

        product.Costo =
            dto.Costo;

        product.PrecioVenta =
            dto.PrecioVenta;

        await _repository
            .UpdateAsync(product);

        return product;
    }
}
