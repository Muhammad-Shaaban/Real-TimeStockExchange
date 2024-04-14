import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './Guards/auth.guard';

const routes: Routes = [
  {path: '', component: LoginComponent, pathMatch: 'full' },
  {path: 'login', component: LoginComponent},
  {path: 'stock', loadChildren: () => import('./Stock/stock.module').then(s => s.StockModule), canActivate: [AuthGuard], canActivateChild: [AuthGuard]},
  {path: 'order', loadChildren: () => import('./order/order.module').then(o => o.OrderModule), canActivate: [AuthGuard], canActivateChild: [AuthGuard]}
];

@NgModule({
  declarations: [],
  exports: [RouterModule],
  imports: [
    RouterModule.forRoot(routes)
  ]
})
export class AppRoutingModule { }