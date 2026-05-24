import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatDialogRef } from "@angular/material/dialog";
import Swal from "sweetalert2";

import { ProductService } from "../../../../core/services/product.service";
import { CreateProductRequest } from '../../../../core/models/create-product-request';
import { ProductMapper } from '../../../../core/mappers/product.mapper';

@Component({
  selector: 'app-product-modal',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  templateUrl: './product-modal.component.html',
  styleUrl: './product-modal.component.scss'
})
export class ProductModalComponent {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private dialogRef = inject(MatDialogRef<ProductModalComponent>);

  form = this.fb.group({

    name: ['',Validators.required],
    nroLote: [''],
    cost: [0,Validators.required],
    salePrice: [0]

  });

  save() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    
    const payload = ProductMapper.toCreateRequest(this.form.getRawValue());

    this.productService.create(payload).subscribe({
      next: () => {
        Swal.fire({
          icon: "success",
          title: "Producto registrado",
        });

        this.dialogRef.close(true);
      },

      error: () => {
        Swal.fire({
          icon: "error",
          title: "Error al guardar",
        });
      },
    });
  }

  close() {
    this.dialogRef.close(false);
  }

}
