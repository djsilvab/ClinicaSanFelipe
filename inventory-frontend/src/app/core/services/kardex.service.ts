import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Kardex } from '../models/kardex';

@Injectable({
  providedIn: 'root'
})
export class KardexService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrls.kardex}/Kardex`;

  getAll(): Observable<Kardex[]> {
    return this.http.get<Kardex[]>(this.apiUrl);
  }
  
}
