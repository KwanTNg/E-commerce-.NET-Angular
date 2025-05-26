import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5203/api/';
  private http = inject(HttpClient);
  //Service is a singleton, properties stored available lifetime
  //types and brands are static lists, should store here
  //shop component eventually disposed when routing
  //avod keep requesting from API
  types: string[] = []
  brands: string[] = []
  
  //products store in component, should subscribe there
  getProducts() {
    return this.http.get<Pagination<Product>>(this.baseUrl + 'products?pageSize=20')
  }

  //since types and brands are stored here, should subscribe here
  getBrands() {
    //to prevent it keep executing
    if (this.brands.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'products/brands').subscribe({
      next: response => this.brands = response
    })
  }

  getTypes() {
    if (this.types.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + 'products/types').subscribe({
      next: response => this.types = response
    })
  }
}
