namespace Product.Api.Application.DTOs;

public class CreateProductDto
{
    public string Nombre_producto { get; set; } = string.Empty;

    public string NroLote { get; set; } = string.Empty;

    public decimal Costo { get; set; }

    public decimal PrecioVenta { get; set; }
}
