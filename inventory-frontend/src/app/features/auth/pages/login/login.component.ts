import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { Router } from '@angular/router';
import {CommonModule} from '@angular/common';
import Swal from 'sweetalert2';

import { AuthService } from '../../../../core/services/auth.service';
import { LoginRequest } from '../../../../core/models/login-request';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);

  loading = false;

  loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  login(){
    if(this.loginForm.invalid){
      this.loginForm.markAllAsTouched();
      return;
    }

    this.loading = true;

    const credentials: LoginRequest = {
      username: this.loginForm.value.username ?? '', 
      password: this.loginForm.value.password ?? ''
    };

    this.authService.login(credentials).subscribe({
      next: () => {
        
        Swal.fire({
          icon: 'success',
          title: 'Bienvenido'          
        });

        this.router.navigate(['/purchase']);
      },
      error: () => {
        Swal.fire({
          icon: 'error',
          title: 'Usuario o contraseña incorrectos'          
        });
        this.loading = false;
      }
    });
  }
}
