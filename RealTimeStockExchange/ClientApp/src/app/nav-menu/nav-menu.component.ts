import { Component, DoCheck } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements DoCheck {
  isExpanded = false;
  isAuthenticated: boolean = false;
  UserName: string = '';


  constructor(private router: Router, private authService: AuthService) {
  }

  ngDoCheck(): void {
    this.isAuthenticated = this.authService.IsAuthenticated();
    this.UserName = localStorage.getItem('userName')!;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  Logout = () => {
    localStorage.clear();
    this.authService.IsAuthenticatedSubject.next(false);
    this.router.navigate(['/'])
  }
}
