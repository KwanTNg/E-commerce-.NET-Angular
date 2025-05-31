import { computed, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Cart, CartItem } from '../../shared/models/cart';
import { Product } from '../../shared/models/product';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  // ng g environments
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  cart = signal<Cart | null>(null);
  itemCount = computed(() => {
    //because the cart could be null, use ?
    return this.cart()?.items.reduce((sum, item) => sum + item.quantity, 0)
  });
  totals = computed(() => {
    const cart = this.cart();
    if (!cart) return null;
    const subtotal = cart.items.reduce((sum, item) => sum + item.price * item.quantity, 0)
    const shipping = 0;
    const discount = 0;
    return {
      subtotal,
      shipping,
      discount,
      total: subtotal + shipping - discount
    }
  })

  getCart(id: string) {
    //if we want to return observable, so that is can be use by another function, use pipe, not subscribe
    return this.http.get<Cart>(this.baseUrl + 'cart?id=' + id).pipe(
      map(cart => {
        this.cart.set(cart);
        return cart;
      })
    )

  }

  setCart(cart: Cart) {
    return this.http.post<Cart>(this.baseUrl + 'cart', cart).subscribe({
      next: cart => this.cart.set(cart)
    })
  }

  addItemToCart(item: CartItem | Product, quantity = 1) {
    //Check if there is a cart, if not create a cart
    //this.cart() is a signal
    const cart = this.cart() ?? this.createCart()
    //Check if it is product before adding to cart
    if (this.isProduct(item)) {
      item = this.mapProductToCartItem(item);
    }
    cart.items = this.addOrUpdateItem(cart.items, item, quantity);
    // Update the database and signal
    this.setCart(cart);
  }

  removeItemFromCart(productId: number, quantity = 1) {
    const cart = this.cart();
    if (!cart) return;
    const index = cart.items.findIndex(x => x.productId === productId);
    //-1 means item not in cart
    if (index !== -1) {
      if (cart.items[index].quantity > quantity) {
        cart.items[index].quantity -= quantity;
      } else {
        //remove the item
        cart.items.splice(index, 1);
      }
      if (cart.items.length === 0) {
        //remove the cart
        this.deleteCart();
      } else {
        //update the cart
        this.setCart(cart);
      }
    }
  }
  deleteCart() {
    this.http.delete(this.baseUrl + 'cart?id=' + this.cart()?.id).subscribe({
      next: () => {
        localStorage.removeItem('cart_id');
        this.cart.set(null);
      }
    })
  }

  private addOrUpdateItem(items: CartItem[], item: CartItem, quantity: number): CartItem[] {
    //Check if the item already exist in our basket
    const index = items.findIndex(x => x.productId === item.productId);
    if (index === -1) {
      item.quantity = quantity;
      items.push(item);
    } else {
      items[index].quantity += quantity
    }
    return items;
  }

  private mapProductToCartItem(item: Product): CartItem {
    return {
      productId: item.id,
      productName: item.name,
      price: item.price,
      quantity: 0,
      pictureUrl: item.pictureUrl,
      brand: item.brand,
      type: item.type
    }
  }

  //return is a boolean, check if the item is Product or not
  private isProduct(item: CartItem | Product): item is Product {
    return (item as Product).id !== undefined;
  }


  private createCart(): Cart {
    //new Cart() will create a new cart
    const cart = new Cart();
    //store card id in local storage
    localStorage.setItem('cart_id', cart.id);
    return cart;
  }
}
