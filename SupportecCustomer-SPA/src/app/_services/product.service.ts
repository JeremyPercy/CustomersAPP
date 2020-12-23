import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../_models/product';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getProducts(id: number): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl + 'products/user/' + id);
  }

  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(this.baseUrl + 'products/' + id);
  }

  addProduct(product: Product) {
    return this.http.post(this.baseUrl + 'products/add', product);
  }
}
