import { Component, inject, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";

import {
  FormArray,
  FormBuilder,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";

import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import Swal from "sweetalert2";
import { ProductService } from "../../../../core/services/product.service";
import { PurchaseService } from "../../../../core/services/purchase.service";
import { Product } from "../../../../core/models/product";
import { ProductModalComponent } from "../../components/product-modal/product-modal.component";
import { PurchaseDetailRequest } from "../../../../core/models/purchase-detail-request";
import { PurchaseRequest } from "../../../../core/models/purchase-request";

@Component({
  selector: "app-purchase-form",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatDialogModule],
  templateUrl: "./purchase-form.component.html",
  styleUrl: "./purchase-form.component.scss",
})
export class PurchaseFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private dialog = inject(MatDialog);
  private productService = inject(ProductService);
  private purchaseService = inject(PurchaseService);
  products: Product[] = [];
  form = this.fb.group({
    details: this.fb.array([]),
  });

  ngOnInit(): void {
    this.loadProducts();
    this.addRow();
  }

  get details(): FormArray {
    return this.form.get("details") as FormArray;
  }

  createItem() {
    return this.fb.group({
      productId: [null, Validators.required],
      quantity: [1, Validators.required],
      price: [0, Validators.required],
      salePrice: [0],
    });
  }

  addRow() {
    this.details.push(this.createItem());
  }

  removeRow(index: number) {
    this.details.removeAt(index);
  }

  loadProducts() {
    this.productService.getAll().subscribe({
      next: (response) => {
        this.products = response;
      },
    });
  }

  getSubtotal(index: number) {
    const row = this.details.at(index);
    const quantity = Number(row.get("quantity")?.value || 0);
    const price = Number(row.get("price")?.value || 0);
    return quantity * price;
  }

  getTotal() {
    return this.details.controls.reduce((sum, row) => {
      const quantity = row.get("quantity")?.value || 0;
      const price = row.get("price")?.value || 0;
      return sum + quantity * price;
    }, 0);
  }

  getIgv(index: number) {
    return this.getSubtotal(index) * 0.18;
  }

  getRowTotal(index: number): number {
    return this.getSubtotal(index) + this.getIgv(index);
  }

  getPurchaseSubtotal(): number {
    return this.details.controls.reduce((sum, _, i) => {
      return sum + this.getSubtotal(i);
    }, 0);
  }

  getPurchaseIgv(): number {
    return this.details.controls.reduce((sum, _, i) => {
      return sum + this.getIgv(i);
    }, 0);
  }

  getPurchaseTotal(): number {
    return this.getPurchaseSubtotal() + this.getPurchaseIgv();
  }

  savePurchase() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    Swal.fire({
      title: "¿Registrar compra?",
      text: "Se registrará la compra.",
      icon: "question",
      showCancelButton: true,
      confirmButtonText: "Sí, registrar",
      cancelButtonText: "Cancelar",
    }).then((result) => {

      if (!result.isConfirmed) {
        return;
      }

      const formValues = this.form.getRawValue();

      const purchasePayload = {
        subTotal: this.getPurchaseSubtotal(),
        igv: this.getPurchaseIgv(),
        total: this.getPurchaseTotal(),
        details: this.details.controls.map(row => {
            const quantity = Number(row.get('quantity')?.value || 0);
            const price = Number(row.get('price')?.value || 0 );
            const subtotal = quantity * price;
            const igv = subtotal * 0.18;
            const total = subtotal + igv;
            return {
              id_producto: row.get('productId')?.value ?? 0,
              cantidad: quantity,
              precio: price,
              sub_Total: subtotal,
              igv: igv,
              total: total
            };
          })
      };

      this.purchaseService.create(purchasePayload).subscribe({
        next: () => {
          Swal.fire({
            icon: "success",
            title: "Compra registrada",
          });

          this.details.clear();

          this.addRow();
        },
        error: (err) => {
          Swal.fire({
            icon: "error",
            title: "Error al registrar la compra",
          });
        }
      });
    });
  }

  openProductModal() {
    const dialogRef = this.dialog.open(ProductModalComponent, {
      width: "650px",
      maxWidth: "95vw",
      panelClass: "custom-dialog",
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadProducts();
      }
    });
  }
}
