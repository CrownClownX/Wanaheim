import { User } from "../../models/user.model";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Store, MemoizedSelector, createFeatureSelector, createSelector } from "@ngrx/store";
import { Login, SignUp, Logout } from "./auth.actions";

export const featureName = 'auth';

export interface AuthState {
  loginFailed: boolean;
  user: User;
}

@Injectable()
export class AuthStore {
  readonly authState$: Observable<AuthState>;

  constructor(private store: Store<AuthState>) {
    if (!this.authState$) {
      this.authState$ = this.store.select(this.getSelector1());
    }
  }

  private getFeatureSelector(): MemoizedSelector<AuthState, any> {
    return createFeatureSelector<AuthState>(featureName);
  }

  private getSelector1(): MemoizedSelector<AuthState, any>{
    return createSelector(
      this.getFeatureSelector(),
      (state: AuthState) => state
    );
  }

  private getSelector(key: keyof any): MemoizedSelector<AuthState, any> {
    return createSelector(
      this.getFeatureSelector(),
      (state: AuthState) => state[key]
    );
  }

  dispatchLoginAction(payload: any): void {
    this.store.dispatch(new Login(payload));
  }

  dispatchLogoutAction(): void {
    this.store.dispatch(new Logout());
  }

  dispatchSignupAction(payload: any): void {
    this.store.dispatch(new SignUp(payload))
  }
}
