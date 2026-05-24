import { Component, Inject } from "@angular/core";
import { CommonModule } from "@angular/common";

import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from "@angular/material/dialog";

import { Kardex } from "../../../../core/models/kardex";

@Component({
  selector: "app-kardex-movements-modal",
  standalone: true,
  imports: [CommonModule, MatDialogModule],
  templateUrl: "./kardex-movements-modal.component.html",
  styleUrl: "./kardex-movements-modal.component.scss",
})
export class KardexMovementsModalComponent {
  constructor(
    @Inject(MAT_DIALOG_DATA)
    public movements: Kardex[],

    private dialogRef: MatDialogRef<KardexMovementsModalComponent>
  ) {}

  close() {
    this.dialogRef.close();
  }
}
