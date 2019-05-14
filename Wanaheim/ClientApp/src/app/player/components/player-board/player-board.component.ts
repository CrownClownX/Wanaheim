import { PlayerService } from './../../../services/player.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-player-board',
  templateUrl: './player-board.component.html',
  styleUrls: ['./player-board.component.css']
})
export class PlayerBoardComponent implements OnInit {

  userId: number;

  constructor(private playerService: PlayerService) { }

  ngOnInit() {
    this.userId = this.playerService.getPlayerId();
  }

}
