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
import { MatPaginator, PageEvent } from '@angular/material/paginator'
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';
import { EmptyStateComponent } from "../../shared/components/empty-state/empty-state.component";

@Component({
  selector: 'app-shop',
  imports: [ProductItemComponent, MatButton, MatIcon, MatMenu,
    MatListOption, MatSelectionList, MatMenuTrigger, MatPaginator, FormsModule, EmptyStateComponent],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  private shopService = inject(ShopService)
  //inject service from Anuglar material
  private dialogService = inject(MatDialog);
  //for pagination, previously without pagination is products: Product[] = [];
  products?: Pagination<Product>;

  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low-High', value: 'priceAsc'},
    {name: 'Price: High-Low', value: 'priceDesc'}
]
//Use class(object) to supply params
shopParams = new ShopParams();
pageSizeOptions = [5,10,15,20]

  ngOnInit(): void {
    this.initializeShop();
  }

  initializeShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProducts();
  }

  resetFilters(){
    this.shopParams = new ShopParams();
    this.getProducts();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response,
      error: error => console.log(error)
    })
  }

  //for search
  onSearchChange() {
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  //for pagination
  handlePageEvent(event: PageEvent) {
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.getProducts();
  }

  //for sorting
  onSortChange(event: MatSelectionListChange) {
    //Since multiple is false, we take only the first value
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.shopParams.sort = selectedOption.value;
      //Due to pagination, need to set this to page 1
      this.shopParams.pageNumber = 1;
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
        //Due to pagination, need to set this to page 1
        this.shopParams.pageNumber = 1;
        this.getProducts();
        }

      }
    })
  }
}
