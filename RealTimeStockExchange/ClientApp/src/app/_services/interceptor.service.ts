import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InterceptorService implements HttpInterceptor {

  constructor() { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    let hasToken = localStorage.getItem('token');
    if(hasToken) {
      const modifiedRequest = request.clone({
        headers: request.headers.set('Authorization', `Bearer ${hasToken}`)
      });

      return next.handle(modifiedRequest);
    }

    return next.handle(request);
  }
}
