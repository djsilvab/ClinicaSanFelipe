import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { LoginRequest } from '../models/login-request';
import { LoginResponse } from '../models/login-response';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private http = inject(HttpClient);

  private apiUrl = `${environment.apiUrls.auth}/auth`;

  login(request: LoginRequest) {

		return this.http.post<LoginResponse>(`${this.apiUrl}/login`,request)
			.pipe( tap(response => {
					localStorage.setItem('token',response.token);
				})
			);
	}

  logout() {
	  localStorage.removeItem('token');
	}

  isAuthenticated(): boolean {
	  return !!localStorage.getItem('token');
	}
  
}
