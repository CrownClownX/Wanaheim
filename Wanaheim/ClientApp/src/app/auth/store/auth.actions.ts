import { NewUser } from './../../models/newUser.model';
import { Action } from '@ngrx/store';
import { User } from '../../models/user.model';
import { LoggedUser } from '../../models/loggedUser';

export enum AuthActionTypes {
  ON_LOGIN = 'AUTH ON_LOGIN',
  LOGIN_SUCCESS = 'AUTH LOGIN_SUCCESS',
  LOGIN_FAILED = 'AUTH LOGIN_FAILED',
  LOGOUT = 'AUTH LOGOUT',
  ON_SIGNUP = 'AUTH ON_SIGNUP',
  SIGNUP_SUCCESS = 'AUTH SIGNUP_SUCCESS',
  SIGNUP_FAILED = 'AUTH SIGNUP_FAILED'
}

export class Login implements Action {
  readonly type = AuthActionTypes.ON_LOGIN;

  constructor(public payload: {email: string, password: string}) {}
}

export class LoginSuccess implements Action {
  readonly type = AuthActionTypes.LOGIN_SUCCESS;

  constructor(public payload: LoggedUser) {}
}

export class LoginFailed implements Action {
  readonly type = AuthActionTypes.LOGIN_FAILED;

  constructor() {}
}

export class Logout implements Action {
  readonly type = AuthActionTypes.LOGOUT;

  constructor() {}
}

export class SignUp implements Action {
  readonly type = AuthActionTypes.ON_SIGNUP;

  constructor(public payload: NewUser){}
}

export class SignUpSuccess implements Action {
  readonly type = AuthActionTypes.SIGNUP_SUCCESS;

  constructor(public payload: {user: User}){}
}

export class SignUpFailed implements Action {
  readonly type = AuthActionTypes.SIGNUP_FAILED

  constructor(){}
}

export type AuthActions = Login | LoginSuccess | LoginFailed | Logout 
  | SignUp | SignUpSuccess | SignUpFailed;
