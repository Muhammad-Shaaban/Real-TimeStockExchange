import { Component, OnInit } from '@angular/core';
import { SignalRService } from './_services/signal-r.service';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit{
  title = 'app';

  constructor(private signalr: SignalRService, private authService: AuthService) {
  }
  ngOnInit(): void {
    if(this.authService.IsAuthenticated()){
      this.signalr.startConnection();
    }
  }
}
