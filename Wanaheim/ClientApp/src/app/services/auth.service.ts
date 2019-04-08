import { LoggedUser } from './../models/loggedUser';
import { NewUser } from './../models/newUser.model';
import { User } from './../models/user.model';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay, tap, catchError } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http:HttpClient ) { }

  getToken(): string {
    const loggedUser = JSON.parse(localStorage.getItem('user'));

    if(loggedUser){
      return loggedUser.token;
    }

    return null;
  }

  login(email: string, password: string) : Observable<LoggedUser>{
    return this.http.post<LoggedUser>('api/login', {email, password});
  }

  signUp(user:NewUser) : Observable<User>{
    return this.http.post<User>('api/signup', user);
  }
}
