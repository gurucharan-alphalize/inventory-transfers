import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InventoryTransfersComponent } from './inventory-transfers/inventory-transfers.component';

const routes: Routes = [
  { path: '', redirectTo: 'inventory-transfers', pathMatch: 'full' },
  { path: 'inventory-transfers', component: InventoryTransfersComponent },
  { path: '**', redirectTo: 'inventory-transfers' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
