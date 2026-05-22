namespace Purchase.Api.Domain.Entities
{
    public class PurchaseCab
    {
        public int Id_CompraCab
        { get; set; }

        public DateTime FecRegistro
        { get; set; }

        public decimal SubTotal
        { get; set; }

        public decimal Igv
        { get; set; }

        public decimal Total
        { get; set; }

        public ICollection<PurchaseDet>
            Details
        { get; set; }
        = new List<PurchaseDet>();
    }
}
