import { inject, Injectable } from '@angular/core';
import { loadStripe, Stripe, StripeAddressElement, StripeAddressElementOptions, StripeElement, StripeElements } from '@stripe/stripe-js';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CartService } from './cart.service';
import { Cart } from '../../shared/models/cart';
import { firstValueFrom, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StripeService {
  baseUrl = environment.apiUrl;
  private cartService = inject(CartService);
  private http = inject(HttpClient);
  private stripePromise : Promise<Stripe | null>;
  private elements?: StripeElements;
  private addressElements?: StripeAddressElement;

  constructor() {
    this.stripePromise = loadStripe(environment.stripePublicKey);
  }

  getStripeInstance() {
    return this.stripePromise;
  }

  //we only want only single instance of stripe
  //becasue stripe return promise, so we use async
  async intializeElements() {
    if (!this.elements) {
      const stripe = await this.getStripeInstance();
      if (stripe) {
        const cart = await firstValueFrom(this.createOrUpdatePaymentIntent());
        this.elements = stripe.elements(
          {clientSecret: cart.clientSecret, appearance: {labels: 'floating'}})
      } else {
        throw new Error('Stripe has not been loaded');
      }
    }
    return this.elements;
  }

  async createAddressElement() {
    if (!this.addressElements) {
      const elements = await this.intializeElements();
        if (elements) {
          const options: StripeAddressElementOptions = {
            mode: 'shipping'
          };
          this.addressElements = elements.create('address', options);
        } else {
          throw new Error('Elements instance has not been loaded');
        }
    }
    return this.addressElements;
  }

  createOrUpdatePaymentIntent(){
    const cart = this.cartService.cart();
    if (!cart) throw new Error('Problem with cart');
    //what we get back is payment intent id and client secret
    return this.http.post<Cart>(this.baseUrl + 'payments/' + cart.id, {}).pipe(
      map(cart => {
        this.cartService.cart.set(cart);
        return cart;
      })
    )
  }
  
}
