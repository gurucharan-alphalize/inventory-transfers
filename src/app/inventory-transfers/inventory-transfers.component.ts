import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { InventoryTransfer, InventoryTransferCreate, InventoryTransferUpdate, InventoryTransferService } from '../services/inventory-transfer.service';

@Component({
  selector: 'app-inventory-transfers',
  templateUrl: './inventory-transfers.component.html',
  styleUrls: ['./inventory-transfers.component.css']
})
export class InventoryTransfersComponent implements OnInit {
  transfers: InventoryTransfer[] = [];
  form!: FormGroup;
  editingTransfer: InventoryTransfer | null = null;

  constructor(private service: InventoryTransferService, private fb: FormBuilder) {}

  ngOnInit(): void {
    this.loadTransfers();
    this.form = this.fb.group({
      fromLocation: ['', Validators.required],
      toLocation: ['', Validators.required],
      productId: [null, [Validators.required, Validators.min(1)]],
      quantity: [1, [Validators.required, Validators.min(1)]],
      transferDate: ['', Validators.required],
      status: ['Pending']
    });
  }

  loadTransfers(): void {
    this.service.getAll().subscribe(data => this.transfers = data);
  }

  startCreate(): void {
    this.editingTransfer = null;
    this.form.reset({ quantity: 1, status: 'Pending' });
  }

  startEdit(transfer: InventoryTransfer): void {
    this.editingTransfer = transfer;
    this.form.patchValue({
      fromLocation: transfer.fromLocation,
      toLocation: transfer.toLocation,
      productId: transfer.productId,
      quantity: transfer.quantity,
      transferDate: transfer.transferDate.slice(0, 10),
      status: transfer.status
    });
  }

  save(): void {
    if (this.form.invalid) return;

    const val = this.form.value;

    if (this.editingTransfer) {
      const updateDto: InventoryTransferUpdate = {
        id: this.editingTransfer.id,
        fromLocation: val.fromLocation,
        toLocation: val.toLocation,
        productId: val.productId,
        quantity: val.quantity,
        status: val.status,
        transferDate: val.transferDate,
      };
      this.service.update(updateDto).subscribe(() => {
        this.loadTransfers();
        this.editingTransfer = null;
        this.form.reset();
      });
    } else {
      const createDto: InventoryTransferCreate = {
        fromLocation: val.fromLocation,
        toLocation: val.toLocation,
        productId: val.productId,
        quantity: val.quantity,
        transferDate: val.transferDate
      };
      this.service.create(createDto).subscribe(() => {
        this.loadTransfers();
        this.form.reset();
      });
    }
  }

  cancel(): void {
    this.editingTransfer = null;
    this.form.reset();
  }

  delete(transfer: InventoryTransfer): void {
    if (confirm(`Delete transfer #${transfer.id}?`)) {
      this.service.delete(transfer.id).subscribe(() => this.loadTransfers());
    }
  }
}