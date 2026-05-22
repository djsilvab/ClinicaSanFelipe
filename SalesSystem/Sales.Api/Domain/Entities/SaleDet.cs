using System.Text.Json.Serialization;
namespace Sales.Api.Domain.Entities
{
    public class SaleDet
    {
        public int Id_VentaDet { get; set; }

        public int Id_VentaCab { get; set; }

        public int Id_producto { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public decimal Sub_Total { get; set; }

        public decimal Igv { get; set; }

        public decimal Total { get; set; }

        [JsonIgnore]
        public SaleCab? SaleCab { get; set; }
    }
}
