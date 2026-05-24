import { Component, inject, OnInit } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { Product } from "../../../../core/models/product";
import { Kardex } from "../../../../core/models/kardex";
import { ProductService } from "../../../../core/services/product.service";
import { KardexService } from "../../../../core/services/kardex.service";
import { KardexMovementsModalComponent } from "../../components/kardex-movements-modal/kardex-movements-modal.component";

@Component({
  selector: "app-kardex-list",
  standalone: true,
  imports: [CommonModule, MatDialogModule],
  templateUrl: "./kardex-list.component.html",
  styleUrl: "./kardex-list.component.scss",
})
export class KardexListComponent implements OnInit {
  private productService = inject(ProductService);
  private kardexService = inject(KardexService);
  private dialog = inject(MatDialog);
  products: Product[] = [];
  kardex: Kardex[] = [];

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.productService.getAll().subscribe((products) => {
      this.kardexService.getAll().subscribe((kardex) => {
        this.kardex = kardex;
        this.products = products.map((product) => {
          const movements = kardex.filter((k) => k.productoId === product.id);
          const latest = movements.at(-1);
          return {
            ...product,
            stock: latest?.stockActual ?? 0,
          };
        });
      });
    });
  }

  openMovements(productId: number) {
    const movements = this.kardex.filter((x) => x.productoId === productId);
    this.dialog.open(KardexMovementsModalComponent, {
      width: "700px",
      data: movements,
    });
  }
}
