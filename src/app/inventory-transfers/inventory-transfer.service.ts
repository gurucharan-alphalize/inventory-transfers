import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface InventoryTransfer {
  id: number;
  fromLocation: string;
  toLocation: string;
  productId: number;
  productName: string;
  quantity: number;
  status: string;
  transferDate: string;
}

export interface InventoryTransferCreate {
  fromLocation: string;
  toLocation: string;
  productId: number;
  quantity: number;
  transferDate: string;
}

export interface InventoryTransferUpdate {
  id: number;
  fromLocation: string;
  toLocation: string;
  productId: number;
  quantity: number;
  status: string;
  transferDate: string;
}

@Injectable({ providedIn: 'root' })
export class InventoryTransferService {
  private baseUrl = 'https://localhost:5001/api/inventorytransfers';

  constructor(private http: HttpClient) {}

  getAll(): Observable<InventoryTransfer[]> {
    return this.http.get<InventoryTransfer[]>(this.baseUrl);
  }

  getById(id: number): Observable<InventoryTransfer> {
    return this.http.get<InventoryTransfer>(`${this.baseUrl}/${id}`);
  }

  create(dto: InventoryTransferCreate): Observable<InventoryTransfer> {
    return this.http.post<InventoryTransfer>(this.baseUrl, dto);
  }

  update(dto: InventoryTransferUpdate): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${dto.id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
