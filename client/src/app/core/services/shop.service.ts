import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Pagination } from '../../shared/models/pagination';
import { Product } from '../../shared/models/product';
import { ShopParams } from '../../shared/models/shopParams';

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
  getProducts(shopParams: ShopParams) {
    //build a query string
    let params = new HttpParams();
    //convert from string [] to string with ,
    if (shopParams.brands.length > 0) {
      params = params.append('brands', shopParams.brands.join(','));
    }
    if (shopParams.types.length > 0) {
      params = params.append('types', shopParams.types.join(','));
    }

    if (shopParams.sort) {
      params = params.append('sort', shopParams.sort);
    }

    //for search
    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    //for pagination
    params = params.append('pageSize', shopParams.pageSize);
    params = params.append('pageIndex', shopParams.pageNumber);

    return this.http.get<Pagination<Product>>(this.baseUrl + 'products', {params})
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
