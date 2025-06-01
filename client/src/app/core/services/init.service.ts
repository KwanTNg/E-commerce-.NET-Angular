import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { forkJoin, of } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  private cartService = inject(CartService);
  private accountService = inject(AccountService);

  //persist the cart, can only use observable, not signal
  init() {
    const cartId = localStorage.getItem('cart_id');
    //of means return observable of something
    const cart$ = cartId ? this.cartService.getCart(cartId) : of(null);

    //forkJoin allows multiple observables to be completed
    return forkJoin({
      cart: cart$,
      user: this.accountService.getUserInfo()
    })
  }
  
}
