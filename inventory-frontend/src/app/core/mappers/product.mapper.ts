import { Product } from '../models/product';
import { ApiProductResponse } from '../models/api-product-response';
import { CreateProductRequest } from '../models/create-product-request';

export class ProductMapper {

  static toModel( api: ApiProductResponse ): Product {

    return {
      id: api.id_producto ?? 0,
      name: api.nombre_producto ?? '',
      nroLote: api.nroLote ?? '',
      registerDate: api.fec_registro ? new Date(api.fec_registro).toLocaleDateString('es-PE') : '',
      cost: api.costo ?? 0,
      salePrice: api.precioVenta ?? 0
    };

  }

  static toCreateRequest( form: any ): CreateProductRequest {
    return {
      nombre_producto: form.name ?? '',
      nroLote: form.nroLote ?? '',
      costo: Number(form.cost ?? 0),
      precioVenta: Number(form.salePrice ?? 0)
    };

  }

}