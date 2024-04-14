import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StockRoutingModule } from './stock-routing.module';
import { GetAllStockComponent } from './get-all-stock/get-all-stock.component';
import { GetStockDetailsComponent } from './get-stock-details/get-stock-details.component';

import { PaginatorModule } from 'primeng/paginator';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';



@NgModule({
  declarations: [
    GetAllStockComponent,
    GetStockDetailsComponent
  ],
  imports: [
    CommonModule,
    StockRoutingModule,
    PaginatorModule, DialogModule, ButtonModule
  ]
})
export class StockModule { }
