import { Component, Input } from '@angular/core';
import { Product } from '../../../shared/models/product';
import { MatCard, MatCardActions, MatCardContent } from '@angular/material/card';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-product-item',
  imports: [ 
    MatCard, 
    MatCardContent,
    CurrencyPipe,
    MatCardActions,
    MatButton,
    MatIcon ],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.scss'
})
export class ProductItemComponent {
  //We need to pass data from parent component (shop) to here, use input
  //make product optional first to avoid error
  @Input() product?: Product;
}
