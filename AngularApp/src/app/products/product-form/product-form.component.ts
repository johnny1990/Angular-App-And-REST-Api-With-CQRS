// src/app/products/product-form.component.ts
// import { Component } from '@angular/core';
// import { ApiService } from '../../api.service';

import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html'
})
export class ProductFormComponent  implements OnInit {

  productForm!: FormGroup;
  productId: number | null = null;

  product: any = {
    name: '',
    price: 0,
    stock: 0
  };

  message: string = '';

  // constructor(private api: ApiService) {}
  constructor(
    private fb: FormBuilder,
    private api: ApiService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: [''],
      price: [0]
    });
    this.productId = Number(this.route.snapshot.paramMap.get('id'));
    if (this.productId) {
      this.api.getProductById(this.productId).subscribe(p => this.productForm.patchValue(p));
    }
  }
  
  onSubmit() {
    const formValue = this.productForm.value;
    if (this.productId) {
      this.api.updateProduct({ ...formValue, id: this.productId }).subscribe(() => {
        this.router.navigate(['/products']);
      });
    } else {
      this.api.addProduct(formValue).subscribe(() => {
        this.router.navigate(['/products']);
      });
    }
  }
}
