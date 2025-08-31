// src/app/orders/order-form.component.ts
import { Component } from '@angular/core';
import { ApiService } from '../../api.service';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
})
export class OrderFormComponent {
  order: any = {
    userId: 1,   // for now, hardcoded; later, you can pull from logged-in user
    products: [
      {
        productId: 0,
        quantity: 1,
        price: 0
      }
    ]
  };

  message: string = '';

  constructor(private api: ApiService) {}

  submitOrder(): void {
    // simple validation
    if (!this.order.userId || this.order.products.length === 0) {
      this.message = 'Please fill all required fields.';
      return;
    }

    this.api.addOrder(this.order).subscribe({
      next: (res) => {
        this.message = `Order created with ID: ${res.orderId}`;
        // reset form
        this.order = {
          userId: 1,
          products: [
            {
              productId: 0,
              quantity: 1,
              price: 0
            }
          ]
        };
      },
      error: (err) => {
        this.message = 'Failed to create order: ' + err.message;
      }
    });
  }
}
