import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable , map } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Product } from '../models/product';
import { CreateProductRequest } from '../models/create-product-request';
import { ApiProductResponse } from '../models/api-product-response';
import { ProductMapper } from '../mappers/product.mapper';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrls.product}/Product`;

  getAll(): Observable<Product[]> {
    return this.http.get<ApiProductResponse[]>(this.apiUrl)
        .pipe( map(products => products.map(ProductMapper.toModel)));
  }

  create(request: CreateProductRequest): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, request);
  }

}
