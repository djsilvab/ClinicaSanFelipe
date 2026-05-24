export interface CreateProductRequest {
  nombre_producto: string;
  nroLote?: string;
  costo: number;
  precioVenta: number;
}