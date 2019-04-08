import { RouterModule } from '@angular/router';
import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { StoreModule } from '@ngrx/store';
import * as fromAuth from './store/auth.reducer';
import { EffectsModule } from '@ngrx/effects';
import { AuthEffects } from './store/auth.effects';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { CookieService } from 'ngx-cookie-service';
import { featureName, AuthStore } from './store/auth.store';
import { reducers } from './store/auth.reducer';

const routes = [
  {path: 'login', component: LoginComponent},
  {path: 'signup', component: SignUpComponent}
];

@NgModule({
  declarations: [LoginComponent, SignUpComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FormsModule,
    ReactiveFormsModule,
    StoreModule.forFeature(featureName, reducers),
    EffectsModule.forFeature([AuthEffects]),
  ],
  exports: [

  ],
  providers: [
    AuthStore,
    AuthService,
    AuthEffects,
    CookieService
  ]
})

export class AuthModule {

}
