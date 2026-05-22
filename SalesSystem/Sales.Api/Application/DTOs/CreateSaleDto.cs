namespace Sales.Api.Application.DTOs
{
    public class CreateSaleDto
    {
        public List<SaleDetailDto> Details { get; set; } = new();
    }
}
