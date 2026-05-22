using Product.Api.Application.DTOs;
using Product.Api.Application.Interfaces;

namespace Product.Api.Decorators;

public class ProductLoggingDecorator : IProductService
{
    private readonly IProductService _service;
    private readonly ILogger<ProductLoggingDecorator> _logger;

    public ProductLoggingDecorator(IProductService service, ILogger<ProductLoggingDecorator> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Domain.Entities.Product> CreateAsync(CreateProductDto dto)
    {
        _logger.LogInformation(
             "Registrando producto {Nombre}",
             dto.Nombre_producto);

        return await _service
            .CreateAsync(dto);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation(
            "Eliminando producto Id {Id}",
            id);

        return await _service
            .DeleteAsync(id);
    }

    public async Task<List<Domain.Entities.Product>> GetAllAsync()
    {
        _logger.LogInformation(
           "Listando productos");

        return await _service
            .GetAllAsync();
    }

    public async Task<Domain.Entities.Product?> UpdateAsync(int id, UpdateProductDto dto)
    {
        _logger.LogInformation(
           "Actualizando producto Id {Id}",
           id);

        return await _service
            .UpdateAsync(id, dto);
    }
}
