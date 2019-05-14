import { Item } from './../../models/item';
import { Player } from './../../models/player.model';
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { Store, MemoizedSelector, createFeatureSelector, createSelector } from "@ngrx/store";

export const featureName = 'player';

export interface PlayerState {
  player: Player;
  itemIds: number[];
  items:{[key:number]: Item};
}

@Injectable()
export class AuthStore {
  readonly playerState$: Observable<PlayerState>;

  constructor(private store: Store<PlayerState>) {
    if (!this.playerState$) {
      this.playerState$ = this.store.select(this.getSelector1());
    }
  }

  private getFeatureSelector(): MemoizedSelector<PlayerState, any> {
    return createFeatureSelector<PlayerState>(featureName);
  }

  private getSelector1(): MemoizedSelector<PlayerState, any>{
    return createSelector(
      this.getFeatureSelector(),
      (state: PlayerState) => state
    );
  }
}