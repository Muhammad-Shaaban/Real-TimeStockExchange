import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GetAllOrderHistoryComponent } from './get-all-order-history/get-all-order-history.component';

const routes: Routes = [
  {path: '', component: GetAllOrderHistoryComponent},
  {path: 'get-history', component: GetAllOrderHistoryComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule { }
