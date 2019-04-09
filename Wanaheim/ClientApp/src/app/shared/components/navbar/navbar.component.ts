import { AuthStore } from './../../../auth/store/auth.store';
import { Component, OnInit, Input } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { Logout } from '../../../auth/store/auth.actions';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  constructor(private authStore:AuthStore) { }

  subscription: Subscription = new Subscription();
  isLoggedIn: boolean;

  ngOnInit() {
    this.subscription.add(
      this.authStore.authState$.subscribe((state) => {

        if(state == undefined){
          this.isLoggedIn = false;
        }
        else{
          this.isLoggedIn = !state.loginFailed;
        }
      })
    )
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();    
  }

  logout(){
    this.authStore.dispatchLogoutAction();
  }
}
