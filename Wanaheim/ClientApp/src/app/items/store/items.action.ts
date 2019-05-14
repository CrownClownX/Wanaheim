import { Action } from "@ngrx/store";

export enum ItemActionTypes{
  LOAD_ITEMS = 'ITEMS LOAD_ITEMS',
  LOAD_ITEMS_SUCCESS = 'ITEMS LOAD_ITEMS_SUCCESS',
  LOAD_ITEMS_FAIL = 'ITEMS LOAD_ITEMS_FAIL'
}

export class LoadItems implements Action {
  readonly type = ItemActionTypes.LOAD_ITEMS;
}

export class LoadItemsSuccess implements Action {
  readonly type = ItemActionTypes.LOAD_ITEMS_SUCCESS;
}

export class LoadItemsFail implements Action {
  readonly type = ItemActionTypes.LOAD_ITEMS_FAIL;
}

export type ItemActions = LoadItems | LoadItemsSuccess | LoadItemsFail;