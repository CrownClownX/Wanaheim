import { StoreModule } from '@ngrx/store';
import { PlayerService } from './../services/player.service';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayerBoardComponent } from './components/player-board/player-board.component';
import { PlayerInfoComponent } from './components/player-info/player-info.component';
import { EquipmentComponent } from './components/equipment/equipment.component';
import { featureName } from './store/player.store';
import { reducers } from './store/player.reducer';

const routes = [
  {path: '', component: PlayerBoardComponent}
]

@NgModule({
  declarations: [PlayerBoardComponent, PlayerInfoComponent, EquipmentComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    StoreModule.forFeature(featureName, reducers)
  ],
  providers:[
    PlayerService
  ]
})
export class PlayerModule { }
