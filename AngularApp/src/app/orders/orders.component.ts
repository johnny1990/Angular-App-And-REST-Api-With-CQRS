import { Component } from '@angular/core';
import { ApiService } from '..//api.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html'
})
export class OrdersComponent {
  newOrder = {
    customerName: '',
    productId: 0,
    quantity: 1
  };

  message = '';

  constructor(private apiService: ApiService) {}

  submitOrder(): void {
    this.apiService.addOrder(this.newOrder).subscribe({
      next: (res) => {
        this.message = `Order placed successfully. Order ID: ${res.orderId}`;
        this.newOrder = { customerName: '', productId: 0, quantity: 1 };
      },
      error: (err) => this.message = 'Error placing order: ' + err.message
    });
  }
}
