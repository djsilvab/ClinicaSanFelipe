using Sales.Api.Application.DTOs;
using Sales.Api.Application.Interfaces;

namespace Sales.Api.Application.Decorators
{
    public class SaleLoggingDecorator : ISaleFacade
    {
        private readonly ISaleFacade _saleFacade;
        private readonly ILogger<SaleLoggingDecorator> _logger;

        public SaleLoggingDecorator(ISaleFacade saleFacade, ILogger<SaleLoggingDecorator> logger)
        {
            _saleFacade = saleFacade;
            _logger = logger;
        }

        public async Task<int> RegisterSaleAsync(CreateSaleDto dto)
        {
            _logger.LogInformation("Iniciando registro de venta");
            var saleId = await _saleFacade.RegisterSaleAsync(dto);
            _logger.LogInformation("Venta registrada correctamente. IdVenta: {IdVenta}", saleId);
            return saleId;
        }
    }
}
