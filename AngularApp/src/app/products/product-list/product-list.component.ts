// src/app/products/product-list.component.ts
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../api.service';

import { Router } from '@angular/router';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit {
  products: any[] = [];
  message: string = '';

    constructor(private api: ApiService, private router: Router) {}


  ngOnInit(): void {
    this.fetchProducts();
  }

  fetchProducts(): void {
    this.api.getAllProducts().subscribe({
      next: (res) => {
        this.products = res;
      },
      error: (err) => {
        this.message = 'Failed to load products: ' + err.message;
      }
    });
  }






   editProduct(productId: number) {
    this.router.navigate(['/products/edit', productId]);
  }

  deleteProduct(id: number) {
    if (confirm('Are you sure you want to delete this product?')) {
      this.api.deleteProduct(id).subscribe(() => this.fetchProducts());
    }
}
}
