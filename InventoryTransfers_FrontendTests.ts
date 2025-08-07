import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { InventoryTransfersComponent } from './inventory-transfers.component';
import { InventoryTransferService } from '../services/inventory-transfer.service';
import { ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';

describe('InventoryTransfersComponent', () => {
  let component: InventoryTransfersComponent;
  let fixture: ComponentFixture<InventoryTransfersComponent>;
  let serviceSpy: jasmine.SpyObj<InventoryTransferService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('InventoryTransferService', ['getAll', 'create', 'update', 'delete']);

    await TestBed.configureTestingModule({
      declarations: [InventoryTransfersComponent],
      imports: [ReactiveFormsModule],
      providers: [{ provide: InventoryTransferService, useValue: spy }]
    }).compileComponents();

    serviceSpy = TestBed.inject(InventoryTransferService) as jasmine.SpyObj<InventoryTransferService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryTransfersComponent);
    component = fixture.componentInstance;
    serviceSpy.getAll.and.returnValue(of([]));
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load transfers on init', () => {
    expect(serviceSpy.getAll).toHaveBeenCalled();
    expect(component.transfers.length).toBe(0);
  });

  it('should call create on save when adding new transfer', fakeAsync(() => {
    serviceSpy.create.and.returnValue(of({
      id: 1,
      fromLocation: 'A',
      toLocation: 'B',
      productId: 1,
      productName: 'Product A',
      quantity: 10,
      status: 'Pending',
      transferDate: '2023-01-01T00:00:00'
    }));

    component.startCreate();
    component.form.setValue({
      fromLocation: 'A',
      toLocation: 'B',
      productId: 1,
      quantity: 10,
      transferDate: '2023-01-01',
      status: 'Pending'
    });

    component.save();
    tick();

    expect(serviceSpy.create).toHaveBeenCalled();
  }));

  it('should call update on save when editing transfer', fakeAsync(() => {
    serviceSpy.update.and.returnValue(of());

    const existingTransfer = {
      id: 1,
      fromLocation: 'A',
      toLocation: 'B',
      productId: 1,
      productName: 'Product A',
      quantity: 20,
      status: 'Pending',
      transferDate: '2023-01-01T00:00:00'
    };

    component.startEdit(existingTransfer);
    component.form.setValue({
      fromLocation: 'A',
      toLocation: 'B',
      productId: 1,
      quantity: 30,
      transferDate: '2023-01-02',
      status: 'Completed'
    });

    component.save();
    tick();

    expect(serviceSpy.update).toHaveBeenCalled();
  }));

  it('should call delete method when confirmed', () => {
    spyOn(window, 'confirm').and.returnValue(true);
    serviceSpy.delete.and.returnValue(of());

    const transfer = { id: 1 } as any;
    component.delete(transfer);

    expect(serviceSpy.delete).toHaveBeenCalledWith(1);
  });

  it('should not call delete method when delete is cancelled', () => {
    spyOn(window, 'confirm').and.returnValue(false);
    serviceSpy.delete.and.returnValue(of());

    const transfer = { id: 1 } as any;
    component.delete(transfer);

    expect(serviceSpy.delete).not.toHaveBeenCalled();
  });
});