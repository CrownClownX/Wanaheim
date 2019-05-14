import { Component, OnInit } from '@angular/core';
import { AuthStore } from './auth/store/auth.store';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
 // styleUrls: ['./app.component.styl']
})
export class AppComponent implements OnInit {
  
  title = 'ClientApp';

  constructor(private authStore:AuthStore){
  }

  ngOnInit(): void {
    this.authStore.dispatchInitAction();
  }

}
