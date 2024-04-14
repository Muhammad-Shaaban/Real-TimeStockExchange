import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GetAllStockComponent } from './get-all-stock/get-all-stock.component';
import { GetStockDetailsComponent } from './get-stock-details/get-stock-details.component';

const routes: Routes = [
  {path: '', component: GetAllStockComponent},
  {path: 'get-all', component: GetAllStockComponent},
  {path: 'get-details/:id', component:  GetStockDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StockRoutingModule { }
