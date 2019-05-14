import { LoadItems } from './items.action';
import { Store, MemoizedSelector, createFeatureSelector, createSelector } from '@ngrx/store';
import { Item } from './../../models/item';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { EntityState } from '@ngrx/entity';

export const featureName = 'items';

export interface ItemsState extends EntityState<Item> {
  
}

@Injectable()
export class AuthStore {
  readonly itemState$: Observable<ItemsState>;

  constructor(private store: Store<ItemsState>) {
    if (!this.itemState$) {
      this.itemState$ = this.store.select(this.getSelector1());
    }
  }

  private getFeatureSelector(): MemoizedSelector<ItemsState, any> {
    return createFeatureSelector<ItemsState>(featureName);
  }

  private getSelector1(): MemoizedSelector<ItemsState, any>{
    return createSelector(
      this.getFeatureSelector(),
      (state: ItemsState) => state
    );
  }

  public dispatchLoadItems(){
    this.store.dispatch(new LoadItems);
  }
}