import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private jwtHelper = new JwtHelperService();
  public isAuthencatedValue: string = '';
  public IsAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  public IsAdmin = new BehaviorSubject<boolean>(false);

  constructor(private router: Router) { 
    this.IsAuthenticatedSubject.next(localStorage.getItem('isAuthenticated') === 'true' ? true : false);
    this.IsAdmin.next(localStorage.getItem('isAdmin') === 'true' ? true : false);
  }

  GetAuthStatus = (): boolean => {
    if (this.jwtHelper.isTokenExpired(localStorage.getItem('token')!)) {
      localStorage.clear();
      this.router.navigate(['/']);
      return false;
    }

    return true;
  }

  IsAuthenticated = () => {
    return this.IsAuthenticatedSubject.getValue();
  }

  CheckIsAdmin = () => {
    return this.IsAdmin.getValue();
  }
}
