
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

const BASE_URL = 'https://localhost:7282/api'; // Adjust if needed

@Injectable({ providedIn: 'root' })
export class ApiService {
  constructor(private http: HttpClient) {}

  // Orders
  addOrder(data: any): Observable<any> {
    return this.http.post(`${BASE_URL}/orders/add-order`, data);
  }

  getAllOrders(): Observable<any[]> {
  return this.http.get<any[]>(`${BASE_URL}/orders/get-all-orders`);
}

  // Products
  getAllProducts(): Observable<any> {
    return this.http.get(`${BASE_URL}/Products/get-all-products`);
  }

  getProductById(id: number): Observable<any> {
    return this.http.get(`${BASE_URL}/Products/get-product/${id}`);
  }

  addProduct(data: any): Observable<any> {
    return this.http.post(`${BASE_URL}/Products/add-product`, data);
  }

  updateProduct(data: any): Observable<any> {
    return this.http.put(`${BASE_URL}/Products/update-product`, data);
  }

  deleteProduct(id: number): Observable<any> {
    return this.http.delete(`${BASE_URL}/Products/delete-product/${id}`);
  }

  getTopSellingProducts(): Observable<any> {
    return this.http.get(`${BASE_URL}/Products/top-selling-products-last-30days-report`);
  }

  // Users
  getTopSpendingUsers(): Observable<any> {
    return this.http.get(`${BASE_URL}/users/top-1000-spending-users`);
  }
}
