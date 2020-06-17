import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from './_models/user';
import { UserService } from './_services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();
  public showBanner = true;

  constructor(private authService: AuthService, private userService: UserService, private route: Router) { }

  ngOnInit() {
    const token = localStorage.getItem('token');
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
      this.userService.currentUser = user;
    }
    if (user) {
      this.userService.currentUser = user;
    }
    this.showBanner = window.location.href.includes('dashboard');
  }

  onToggleBanner(show: boolean) {
    this.showBanner = show;
  }

  loggedId(): boolean {
    return this.authService.loggedIn();
  }
}
