import { Router, CanActivate } from '@angular/router';
import { AuthService } from './auth.service';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService,private router: Router) {}

  canActivate() {

    if(!this.authService.getToken())
    {
       this.router.navigate(['/auth/login']);
       return false;
    }

    return true;
  }
}