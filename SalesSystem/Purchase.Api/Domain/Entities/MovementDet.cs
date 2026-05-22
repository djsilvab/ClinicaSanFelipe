namespace Purchase.Api.Domain.Entities
{
    public class MovementDet
    {
        public int Id_MovimientoDet
        { get; set; }

        public int Id_movimientocab
        { get; set; }

        public int Id_Producto
        { get; set; }

        public int Cantidad
        { get; set; }

        public MovementCab?
            MovementCab
        { get; set; }
    }
}
