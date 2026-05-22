namespace Sales.Api.Domain.Entities
{
    public class SaleCab
    {
        public int Id_VentaCab { get; set; }

        public DateTime fecRegistro { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Igv { get; set; }

        public decimal Total { get; set; }

        public ICollection<SaleDet> Details { get; set; } = new List<SaleDet>();
    }
}
