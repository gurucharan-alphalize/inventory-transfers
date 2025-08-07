import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { InventoryTransfersComponent } from './inventory-transfers/inventory-transfers.component';

@NgModule({
  declarations: [AppComponent, InventoryTransfersComponent],
  imports: [BrowserModule, ReactiveFormsModule, HttpClientModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
