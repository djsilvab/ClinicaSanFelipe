import { Component, inject, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";

import {
  FormArray,
  FormBuilder,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";

import Swal from "sweetalert2";
import { Product } from "../../../../core/models/product";
import { ProductService } from "../../../../core/services/product.service";
import { KardexService } from "../../../../core/services/kardex.service";
import { SaleService } from "../../../../core/services/sale.service";

@Component({
  selector: "app-sale-form",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: "./sale-form.component.html",
  styleUrl: "./sale-form.component.scss",
})
export class SaleFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private productService = inject(ProductService);
  private kardexService = inject(KardexService);
  private saleService = inject(SaleService);

  products: Product[] = [];

  form = this.fb.group({
    details: this.fb.array([]),
  });

  ngOnInit(): void {
    this.loadInitialData();
    this.addRow();
  }

  get details(): FormArray {
    return this.form.get("details") as FormArray;
  }

  createItem() {
    const group = this.fb.group({
      productId: [null, Validators.required],
      quantity: [1, Validators.required],
      price: [0],
      stock: [0],
    });

    group.get("productId")?.valueChanges.subscribe((productId) => {
      const product = this.products.find((p) => p.id == productId);

      if (product) {
        group.patchValue({
          price: product.salePrice,
          stock: product.stock,
        });
      }
    });

    return group;
  }

  addRow() {
    this.details.push(this.createItem());
  }

  removeRow(index: number) {
    this.details.removeAt(index);
  }

  loadInitialData() {
    this.productService.getAll().subscribe((products) => {
      this.kardexService.getAll().subscribe((kardex) => {
        this.products = products.map((product) => {
          const movements = kardex.filter((k) => k.productoId === product.id);
          const latestMovement = movements.at(-1);

          return {
            ...product,
            stock: latestMovement?.stockActual ?? 0,
          };
        });
      });
    });
  }

  getAvailableStock(index: number) {
    return this.details.at(index).get("stock")?.value || 0;
  }

  validateStock(index: number) {
    const row = this.details.at(index);
    const quantity = Number(row.get("quantity")?.value || 0);
    const stock = Number(row.get("stock")?.value || 0);

    if (quantity > stock) {
      Swal.fire({
        icon: "warning",
        title: "Stock insuficiente",
        text: "La cantidad no puede ser mayor al stock disponible",
      });

      row.patchValue({
        quantity: stock,
      });
    }
  }

  getSubtotal(index: number) {
    const row = this.details.at(index);
    return (row.get("quantity")?.value || 0) * (row.get("price")?.value || 0);
  }

  getIgv(index: number) {
    return this.getSubtotal(index) * 0.18;
  }

  getTotal(index: number) {
    return this.getSubtotal(index) + this.getIgv(index);
  }

  getSaleSubtotal(): number {

    return this.details.controls
      .reduce((sum, _, i) => {

        return sum +
          this.getSubtotal(i);

      }, 0);

  }

  getSaleIgv(): number {

    return this.details.controls
      .reduce((sum, _, i) => {

        return sum +
          this.getIgv(i);

      }, 0);

  }

  getSaleTotal(): number {

    return this.details.controls
      .reduce((sum, _, i) => {

        return sum +
          this.getTotal(i);

      }, 0);

  }

  getGlobalSubtotal(): number {
    return this.details.controls.reduce((sum, _, i) => sum + this.getSubtotal(i), 0);
  }

  getGlobalIgv(): number {
    return this.details.controls.reduce((sum, _, i) => sum + this.getIgv(i), 0);
  }

  getGlobalTotal(): number {
    return this.details.controls.reduce((sum, _, i) => sum + this.getTotal(i), 0);
  }

  saveSale() {

    if (this.form.invalid) {

      this.form.markAllAsTouched();

      return;
    }

    const invalidStock =
      this.details.controls.some(
        row => {

          const quantity =
            Number(
              row.get('quantity')
                ?.value || 0
            );

          const stock =
            Number(
              row.get('stock')
                ?.value || 0
            );

          return quantity > stock;

        }
      );

    if (invalidStock) {

      Swal.fire({
        icon: 'warning',
        title:
          'Stock insuficiente',
        text:
          'Existe un producto con cantidad mayor al stock disponible'
      });

      return;
    }

    Swal.fire({
      title:
        '¿Registrar venta?',
      text:
        'Se registrará la venta',
      icon: 'question',
      showCancelButton: true,
      confirmButtonText:
        'Sí, registrar',
      cancelButtonText:
        'Cancelar'
    })
      .then(result => {

        if (!result.isConfirmed) {
          return;
        }

        const payload = {

          subTotal:
            this.getSaleSubtotal(),

          igv:
            this.getSaleIgv(),

          total:
            this.getSaleTotal(),

          details:

            this.details.controls
              .map(row => {

                const quantity =
                  Number(
                    row.get('quantity')
                      ?.value || 0
                  );

                const price =
                  Number(
                    row.get('price')
                      ?.value || 0
                  );

                const subtotal =
                  quantity * price;

                const igv =
                  subtotal * 0.18;

                const total =
                  subtotal + igv;

                return {

                  id_producto:
                    row.get('productId')
                      ?.value,

                  cantidad:
                    quantity,

                  precioVenta:
                    price,

                  sub_Total:
                    subtotal,

                  igv:
                    igv,

                  total:
                    total

                };

              })

        };

        this.saleService
          .create(payload)
          .subscribe({

            next: () => {

              Swal.fire({
                icon: 'success',
                title:
                  'Venta registrada'
              });

              this.form.reset();

              this.details.clear();

              this.addRow();

              this.loadInitialData();

            },

            error: () => {

              Swal.fire({
                icon: 'error',
                title:
                  'Error al registrar venta'
              });

            }

          });

      });

  }

}
