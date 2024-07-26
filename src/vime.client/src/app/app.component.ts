import { Component, OnInit } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { UserService } from './users/services/user.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  isAuthenticated: any;
  username: any;
  constructor(private userService: UserService) {}
  ngOnInit() {
    this.userService.userData$.subscribe((userData) => {
      this.isAuthenticated = userData.isAuthenticated;
      this.username = userData.username;
    });
  }

  logout(){
    this.userService.logout();
  }
}
