
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

// const routes: Routes = [
//   {
//     path: 'orders',
//     loadChildren: () =>
//       import('./orders/orders.module').then(m => m.OrdersModule),
//   },
//   {
//     path: 'products',
//     loadChildren: () =>
//       import('./products/products.module').then(m => m.ProductsModule),
//   },
//   {
//     path: 'users',
//     loadChildren: () =>
//       import('./users/users.module').then(m => m.UsersModule),
//   },
//   { path: '', redirectTo: '/orders', pathMatch: 'full' },
// ];
const routes: Routes = [
  { path: 'orders', loadChildren: () => import('./orders/orders.module').then(m => m.OrdersModule) },
  { path: 'products', loadChildren: () => import('./products/products.module').then(m => m.ProductsModule) },
  { path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule) },
  { path: '', redirectTo: '/orders', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
