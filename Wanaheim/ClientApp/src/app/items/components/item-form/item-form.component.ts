import { Observable, forkJoin } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { ItemsService } from '../../../services/items.service';
import { ItemSave, Item } from '../../../models/item';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-item-form',
  templateUrl: './item-form.component.html',
  styleUrls: ['./item-form.component.styl']
})
export class ItemFormComponent implements OnInit {

  categories: any[];
  subcategories: any[];

  item: ItemSave = {
    id: 0,
    name: '',
    price: 0,
    quantity: 0,
    description: '',
    categoryId: 0,
    subcategoryId: 0
  };

  constructor(
    private service: ItemsService,
    private route: ActivatedRoute) {

    route.params.subscribe(p => {
      this.item.id = +p['id'] || 0;
    });
  }

  ngOnInit() {
    var sources = [
      this.service.getCategories()
    ];

    if(this.item.id){
      sources.push(this.service.getItem(this.item.id));
    }

    forkJoin(sources).subscribe(data => {
      this.categories = data[0];

      if(this.item.id){
        this.setItem(data[1]);
        this.populateSubcategories();
      }
    })
  }

  private setItem(i: Item) {
    this.item.id = i.id;
    this.item.name = i.name;
    this.item.price = i.price;
    this.item.quantity = i.quantity;
    this.item.description = i.description

    this.item.categoryId = i.category.id;
    this.item.subcategoryId = i.subcategory.id;
}

  onCategoryChange() {
    this.populateSubcategories();

    delete this.item.subcategoryId;
  }

  private populateSubcategories() {
    var currentCategory = this.categories
      .find(c => c.id == this.item.categoryId);

    this.subcategories = currentCategory
      ? currentCategory.subcategories
      : [];
  }

  submit() {
    var result$ = (this.item.id)
      ? this.service.updateItem(this.item)
      : this.service.createItem(this.item);

    result$.subscribe(x => {
        console.log("success", x);
      });
  }


}
