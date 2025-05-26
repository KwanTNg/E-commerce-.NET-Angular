import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from './filters-dialog/filters-dialog.component';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu'
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list'
import { ShopParams } from '../../shared/models/shopParams';


@Component({
  selector: 'app-shop',
  imports: [ProductItemComponent, MatButton, MatIcon, MatMenu,
    MatListOption, MatSelectionList, MatMenuTrigger
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService)
  //inject service from Anuglar material
  private dialogService = inject(MatDialog);
  products: Product[] = [];

  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low-High', value: 'priceAsc'},
    {name: 'Price: High-Low', value: 'priceDesc'}
]
//Use class(object) to supply params
shopParams = new ShopParams();

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProducts();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response.data,
      error: error => console.log(error)
    })
  }

  //for sorting
  onSortChange(event: MatSelectionListChange) {
    //Since multiple is false, we take only the first value
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.shopParams.sort = selectedOption.value;
      this.getProducts();
    }
  }

  //Brands, types selected in dialog, data 
  openFiltersDialog() {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types
      }
    });
    dialogRef.afterClosed().subscribe({
      //Result data is from FiltersDialogComponent
      next: result => {
        if (result) {
        this.shopParams.brands = result.selectedBrands;
        this.shopParams.types = result.selectedTypes;
        this.getProducts();
        }

      }
    })
  }
}
