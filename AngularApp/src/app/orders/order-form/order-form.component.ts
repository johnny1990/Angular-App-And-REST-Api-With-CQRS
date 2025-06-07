// src/app/orders/order-form.component.ts
import { Component } from '@angular/core';
import { ApiService } from '../../api.service';
@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
})
export class OrderFormComponent {
  order: any = {
    customerName: '',
    productName: '',
    quantity: 1,
    orderDate: new Date()
  };

  message: string = '';

  constructor(private api: ApiService) {}

  submitOrder(): void {
    if (!this.order.customerName || !this.order.productName) {
      this.message = 'Please fill all required fields.';
      return;
    }

    this.api.addOrder(this.order).subscribe({
      next: (res) => {
        this.message = `Order created with ID: ${res.orderId}`;
        this.order = {
          customerName: '',
          productName: '',
          quantity: 1,
          orderDate: new Date()
        };
      },
      error: (err) => {
        this.message = 'Failed to create order: ' + err.message;
      }
    });
  }
}
