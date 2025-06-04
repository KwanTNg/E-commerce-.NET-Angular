import { inject, Injectable } from '@angular/core';
import { CartService } from './cart.service';
import { forkJoin, of, tap } from 'rxjs';
import { AccountService } from './account.service';
import { SignalrService } from './signalr.service';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  private cartService = inject(CartService);
  private accountService = inject(AccountService);
  private signalrService = inject(SignalrService);

  //persist the cart, can only use observable, not signal
  init() {
    const cartId = localStorage.getItem('cart_id');
    //of means return observable of something
    const cart$ = cartId ? this.cartService.getCart(cartId) : of(null);

    //forkJoin allows multiple observables to be completed
    return forkJoin({
      cart: cart$,
      user: this.accountService.getUserInfo().pipe(
        //check if user is login before making connection to signal R
        tap(user => {
          if (user) this.signalrService.createHubConnection();
            
          
        })
      )
    })
  }
  
}
