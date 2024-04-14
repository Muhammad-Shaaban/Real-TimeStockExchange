import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderRoutingModule } from './order-routing.module';
import { GetAllOrderHistoryComponent } from './get-all-order-history/get-all-order-history.component';


import { PaginatorModule } from 'primeng/paginator';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    GetAllOrderHistoryComponent
  ],
  imports: [
    CommonModule,
    OrderRoutingModule,
    FormsModule,
    PaginatorModule, DialogModule, ButtonModule, DropdownModule
  ]
})
export class OrderModule { }
