namespace Purchase.Api.Application.DTOs
{
    public class CreatePurchaseDto
    {
        public List<PurchaseDetailDto>  Details { get; set; } = new();
    }
}
