namespace Sales.Api.Application.DTOs
{
    public class KardexDto
    {
        public int ProductoId { get; set; }

        public DateTime Fecha { get; set; }

        public string TipoMovimiento { get; set; } = string.Empty;

        public int Cantidad { get; set; }

        public int DocumentoOrigen { get; set; }

        public int StockActual { get; set; }
    }
}
