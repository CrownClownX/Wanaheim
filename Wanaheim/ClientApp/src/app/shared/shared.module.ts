import { AuthEffects } from './../auth/store/auth.effects';
import { AuthActions } from './../auth/store/auth.actions';
import { AuthStore } from './../auth/store/auth.store';
import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/navbar/navbar.component';

@NgModule({
  declarations: [NavbarComponent],
  imports: [
    CommonModule,
    RouterModule
  ], 
  exports: [
    NavbarComponent
  ],
  providers: [
    AuthStore,
    AuthEffects
  ]
})
export class SharedModule { }
