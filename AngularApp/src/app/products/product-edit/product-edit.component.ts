import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../api.service';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html'
})
export class ProductEditComponent implements OnInit {
  productId!: number;
  product: any = {};

  constructor(private route: ActivatedRoute, private api: ApiService) {}

  ngOnInit(): void {
    this.productId = Number(this.route.snapshot.paramMap.get('id'));
    this.api.getProductById(this.productId).subscribe({
      next: (res) => this.product = res,
      error: (err) => console.error('Failed to load product', err)
    });
  }

  saveProduct(): void {
    this.api.updateProduct({ ...this.product, id: this.productId }).subscribe({
      next: () => alert('Product updated successfully!'),
      error: (err) => alert('Update failed: ' + err.message)
    });
  }
}