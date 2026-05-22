namespace Purchase.Api.Domain.Entities
{
    public class MovementCab
    {
        public int Id_MovimientoCab
        { get; set; }

        public DateTime Fec_registro
        { get; set; }

        public int Id_TipoMovimiento
        { get; set; }

        public int Id_DocumentoOrigen
        { get; set; }

        public ICollection<MovementDet>
            Details
        { get; set; }
        = new List<MovementDet>();
    }
}
