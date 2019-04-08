import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ItemSave, Item } from '../models/item';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {

  constructor(private http: HttpClient) {

  }

  //CATEGORIES GET
  getCategories(): Observable<any> {
    return this.http.get<any>('api/categories');
  }

  getItem(id): Observable<Item> {
    return this.http.get<Item>('api/items/' + id)
  }

  //GET MANY
  getItems(query): Observable<any> {
    return this.http.get<any>('api/items/' + '?' + this.toQueryString(query))
  }

  //POST
  createItem(item: ItemSave): Observable<Item> {
    return this.http.post<Item>('api/items', item)
  }

  //PUT
  updateItem(item: ItemSave) {
    return this.http.put('api/items/' + item.id, item);
  }

  //DELETE
  delete(id) {
    return this.http.delete('api/items/' + id);
  }

  toQueryString(obj) {
    var parts = [];

    for (var property in obj) {
      var value = obj[property];

      if (value != null && value != undefined) {
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
      }
    }

    return parts.join('&');
  }
}
