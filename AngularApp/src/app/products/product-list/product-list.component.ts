// src/app/products/product-list.component.ts
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../api.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html'
})
export class ProductListComponent implements OnInit {
  products: any[] = [];
  message: string = '';

  constructor(private api: ApiService) {}

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
}
