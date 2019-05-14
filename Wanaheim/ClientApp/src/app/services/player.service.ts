import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  constructor() { }

  getPlayerId(): number {
    const loggedUser = JSON.parse(localStorage.getItem('user'));

    if(loggedUser){
      return loggedUser.user.id;
    }

    return null;
  }
}
