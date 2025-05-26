import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';


@Component({
  selector: 'app-shop',
  imports: [ProductItemComponent, MatButton, MatIcon],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService)
  //inject service from Anuglar material
  private dialogService = inject(MatDialog);
  products: Product[] = [];
  //Save which brands and types are selected in dialog
  selectedBrands: string[] = [];
  selectedTypes: string[] = [];

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.shopService.getProducts().subscribe({
      next: response => this.products = response.data,
      error: error => console.log(error)
    })
  }

  //Brands, types selected in dialog, data 
  openFiltersDialog() {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.selectedBrands,
        selectedTypes: this.selectedTypes
      }
    });
    dialogRef.afterClosed().subscribe({
      //Result data is from FiltersDialogComponent
      next: result => {
        this.selectedBrands = result.selectedBrands;
        this.selectedTypes = result.selectedTypes;
        //apply filters, get products from DB
        this.shopService.getProducts(this.selectedBrands, this.selectedTypes).subscribe({
          next: response => this.products = response.data,
          error: error => console.log(error)
        })

      }
    })
  }
}
