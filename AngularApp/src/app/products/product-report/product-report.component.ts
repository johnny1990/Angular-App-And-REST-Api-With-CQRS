// src/app/products/product-report.component.ts
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../api.service';

@Component({
  selector: 'app-product-report',
  templateUrl: './product-report.component.html'
})
export class ProductReportComponent implements OnInit {
  reportData: any[] = [];
  message: string = '';

  constructor(private api: ApiService) {}

  ngOnInit(): void {
    this.fetchReport();
  }

  fetchReport(): void {
    this.api.getTopSellingProducts().subscribe({
      next: (data) => {
        this.reportData = data;
      },
      error: (err) => {
        this.message = 'Failed to fetch report: ' + err.message;
      }
    });
  }
}
