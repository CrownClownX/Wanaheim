import { Injectable, Injector } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';
import { HttpEvent, HttpHandler, HttpRequest, HttpInterceptor } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptor implements HttpInterceptor  {

  private authService: AuthService;
  constructor(private injector: Injector) {}
  
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.authService = this.injector.get(AuthService);
    const token: string = this.authService.getToken();

    if(token){
      request = request.clone({
        headers: request.headers.append(
          'Authorization', `Bearer ${token}`
        )
      });
    }

    return next.handle(request);
  }
}

