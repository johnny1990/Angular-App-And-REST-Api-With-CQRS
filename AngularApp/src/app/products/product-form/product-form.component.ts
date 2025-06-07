// src/app/products/product-form.component.ts
import { Component } from '@angular/core';
import { ApiService } from '../../api.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html'
})
export class ProductFormComponent {
  product: any = {
    name: '',
    price: 0,
    stock: 0
  };

  message: string = '';

  constructor(private api: ApiService) {}

  submitProduct(): void {
    if (!this.product.name || this.product.price <= 0) {
      this.message = 'Please fill all required fields.';
      return;
    }

    this.api.addProduct(this.product).subscribe({
      next: (res) => {
        this.message = `Product created with ID: ${res.productId}`;
        this.product = { name: '', price: 0, stock: 0 };
      },
      error: (err) => {
        this.message = 'Failed to add product: ' + err.message;
      }
    });
  }
}
