import { AuthState } from './auth.store';
import { Action } from '@ngrx/store';
import { User } from '../../models/user.model';
import { AuthActionTypes } from './auth.actions';


const initialAuthState: AuthState = {
  loginFailed: true,
  user: null
};

function loginReducer(state: AuthState = initialAuthState, action): AuthState {
  switch (action.type) {
    case AuthActionTypes.LOGIN_SUCCESS:
      return {
        loginFailed: false,
        user: action.payload.user
      };

    case AuthActionTypes.LOGIN_FAILED:
      return {
        loginFailed: true,
        user: null
      };

    case AuthActionTypes.LOGOUT:
      return initialAuthState;

    case AuthActionTypes.SIGNUP_SUCCESS:
      return {
        loginFailed: false,
        user: action.payload.user
      }

    case AuthActionTypes.SIGNUP_FAILED:
      return {
        loginFailed: true,
        user: null
      }

    default:
      return state;
  }
}

export const reducers = loginReducer

