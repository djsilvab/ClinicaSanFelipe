using System.Text.Json.Serialization;

namespace Purchase.Api.Domain.Entities
{
    public class PurchaseDet
    {
        public int Id_CompraDet { get; set; }

        public int Id_CompraCab { get; set; }

        public int Id_producto { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public decimal Sub_Total { get; set; }

        public decimal Igv { get; set; }

        public decimal Total { get; set; }

        [JsonIgnore]
        public PurchaseCab? PurchaseCab { get; set; }
    }
}
