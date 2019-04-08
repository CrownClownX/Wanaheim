import { Component, OnInit } from '@angular/core';
import { ItemsService } from '../../../services/items.service';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.styl']
})
export class ItemsComponent implements OnInit {

  items: any[];
  categories: any[];
  query: any = {
      pageSize: 3
  };
  columns = [
      { title: 'Name', key: 'name', isSortable: true},
      { title: 'Price', key: 'price', isSortable: true},
      { title: 'Quantity', key: 'quantity'},
      { title: 'Description', key: 'description'},
      { title: 'Class', key: 'subcategory', isSortable: true },
  ];

  /** items ctor */
  constructor(private service: ItemsService) {

  }

  ngOnInit() {
      this.populateItems(); 

      this.service.getCategories()
          .subscribe(categories => this.categories = categories);
  }

  onCategoryChange() {
      if (this.query.categoryId == 0) {
          this.query.categoryId = null;
      }

      this.query.page = 1;
      this.populateItems(); 
  }

  sortBy(column: string) {
      if (this.query.sortBy == column) {
          this.query.ifAscending = !this.query.ifAscending;
      } else {
          this.query.ifAscending = true;
          this.query.sortBy = column;
      }

      this.populateItems(); 
  }

  populateItems() {
      this.service.getItems(this.query)
          .subscribe(queryResult => {
              this.items = queryResult.entities;
              this.query.itemsCount = queryResult.count;
          });
  }

  onPageChange(page) {
      this.query.page = page;
      this.populateItems(); 
  }

}
