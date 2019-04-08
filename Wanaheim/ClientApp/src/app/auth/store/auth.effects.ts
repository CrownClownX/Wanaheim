import { AuthService } from './../../services/auth.service';
import { Router } from '@angular/router';
import { Login, AuthActionTypes, Logout, LoginSuccess, LoginFailed, SignUp, SignUpSuccess, SignUpFailed } from './auth.actions';
import { tap, mergeMap, map, catchError } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { defer, of, Observable } from 'rxjs';
import { INIT, Action } from '@ngrx/store';
import { LoggedUser } from '../../models/loggedUser';


@Injectable()
export class AuthEffects  {
  @Effect()
  onLogin$ = this.actions$.pipe(
    ofType<Login>(AuthActionTypes.ON_LOGIN),
    mergeMap((action) => {
       return this.authService.login(action.payload.email,
          action.payload.password).pipe(
            map((user) => {
              if (user) {
                return new LoginSuccess(user);
              }

              return new LoginFailed();
            }),
            catchError(() => {
              return of(new LoginFailed());
            })
          );
      })
  );

  @Effect({dispatch: false})
  LoginSuccess$: Observable<any> = this.actions$.pipe(
    ofType<LoginSuccess>(AuthActionTypes.LOGIN_SUCCESS),
    tap((user) => {
      localStorage.setItem('user', JSON.stringify(user.payload));
      this.router.navigateByUrl('/');
    })
  );

  @Effect({dispatch: false})
  Logout$: Observable<any> = this.actions$.pipe(
    ofType<Logout>(AuthActionTypes.LOGOUT),
    tap((user) => {
      localStorage.removeItem('user');
      this.router.navigateByUrl('/auth/login');
    })
  );

  @Effect()
  onSignUp$ = this.actions$.pipe(
    ofType<SignUp>(AuthActionTypes.ON_SIGNUP),
    mergeMap((action) => {
       return this.authService.signUp(action.payload).pipe(
            map((user) => {
              if (user) {
                return new SignUpSuccess({user});
              }

              return new SignUpFailed();
            }),
            catchError(() => {
              return of(new SignUpFailed());
            })
          );
      })
  );

  @Effect()
  init$ : Observable<any> = this.actions$.pipe(
    ofType(INIT),
    map(() => {
      console.log("sdd");
      const user: any = localStorage.getItem('user');
      if(user){
        return new LoginSuccess(user);
      }

      return new Logout();
    })
  );

  constructor(private actions$: Actions,
              private router: Router,
              private authService: AuthService) {}
}