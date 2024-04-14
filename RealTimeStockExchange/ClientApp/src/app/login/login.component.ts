import { Component, OnInit } from '@angular/core';
import { AuthDTO, LoginDTO, SwaggerClient } from '../_services/swagger/SwaggerClient.service';
import { NotificationService } from '../_services/notification.service';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { SignalRService } from '../_services/signal-r.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
    
  public LoginForm: LoginDTO = new LoginDTO({email: '', password: '' });

  constructor(
    private service: SwaggerClient,
    private tostar: NotificationService,
    private router: Router,
    private authService: AuthService,
    private signalr: SignalRService) {
  }
  ngOnInit(): void {
    if(this.authService.IsAuthenticated()){
      this.router.navigate(['/stock']);
    }
  }

  Login = () => {
    this.service.apiAccountLoginPost(this.LoginForm).subscribe({
      next: (res: AuthDTO) => {
        if(!res.isAuthenticated){
          this.tostar.showError(res.message!);
        } else {
        
          this.tostar.showSuccess(res.message!)

          localStorage.setItem('token', res.token!);
          localStorage.setItem('userName', res.userName!);
          localStorage.setItem('isAuthenticated', res.isAuthenticated ? 'true' : 'false');
          localStorage.setItem('isAdmin', res.isAdmin ? 'true' : 'false');

          setTimeout(() => {
            this.router.navigate(['/stock']);
          }, 1500);

          this.authService.IsAuthenticatedSubject.next(res.isAuthenticated);
          this.authService.IsAdmin.next(res.isAdmin!);

          this.signalr.startConnection()
        }
      }, 
      error: (err) => {
        this.tostar.showSuccess(err.error)
      }
    })
  }
}
