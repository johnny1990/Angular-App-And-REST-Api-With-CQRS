import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderFormComponent } from './order-form/order-form.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';

const routes: Routes = [
  { path: '', component: OrderListComponent },
  { path: 'add', component: OrderFormComponent },
];

@NgModule({
  declarations: [OrderListComponent, OrderFormComponent],
  imports: [CommonModule, FormsModule, RouterModule.forChild(routes)],
})
export class OrdersModule {}
