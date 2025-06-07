import { Component, OnInit } from '@angular/core';
import { ApiService } from '..//api.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html'
})
export class ProductsComponent implements OnInit {
  products: any[] = [];
  selectedProduct: any = null;
  message = '';

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.apiService.getAllProducts().subscribe({
      next: (data) => this.products = data,
      error: (err) => console.error('Error loading products', err)
    });
  }

  editProduct(product: any): void {
    this.selectedProduct = { ...product };
  }

  updateProduct(): void {
    this.apiService.updateProduct(this.selectedProduct).subscribe({
      next: () => {
        this.message = 'Product updated successfully.';
        this.selectedProduct = null;
        this.loadProducts();
      },
      error: (err) => this.message = 'Update failed: ' + err.message
    });
  }

  deleteProduct(id: number): void {
    if (confirm('Are you sure to delete this product?')) {
      this.apiService.deleteProduct(id).subscribe({
        next: () => {
          this.message = 'Product deleted successfully.';
          this.loadProducts();
        },
        error: (err) => this.message = 'Delete failed: ' + err.message
      });
    }
  }
}
