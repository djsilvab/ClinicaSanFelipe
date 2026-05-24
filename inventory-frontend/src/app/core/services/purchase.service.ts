import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { PurchaseRequest } from '../models/purchase-request';


@Injectable({
  providedIn: 'root'
})
export class PurchaseService {

  private http = inject(HttpClient);

  private apiUrl = `${environment.apiUrls.purchase}/purchase`;

  create(request: PurchaseRequest) {
    return this.http.post(this.apiUrl, request);
  }

}
