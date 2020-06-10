import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

import { UserLogin } from '../_models/userLogin';
import { UserRegister } from '../_models/userRegister';
import { UserService } from './user.service';
import { Security } from '../_models/security';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })};

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  private admin = 'ADMIN';
  private member = 'MEMBER';
  private ca = 'CA';
  private orga = 'ORGA';
  private player = 'PLAYER';

  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient, private userService: UserService) { }

  login(model: UserLogin) {
    return this.http.post(this.baseUrl + 'login', model, httpOptions)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            localStorage.setItem('user', JSON.stringify(user.appUser));
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            this.userService.currentUser = user.appUser;
          }
        })
      );
  }

  changePassword(model: Security) {
    debugger;
    return this.http.post(this.baseUrl + this.userService.currentUser.id, model);
  }

  isAdmin(): boolean {
    return this.decodedToken.role.includes(this.admin);
  }

  isMember(): boolean {
    return this.decodedToken.role.includes(this.member);
  }

  isCa(): boolean {
    return this.decodedToken.role.includes(this.ca);
  }

  isOrga(): boolean {
    return this.decodedToken.role.includes(this.orga);
  }

  isPlayer(): boolean {
    return this.decodedToken.role.includes(this.player);
  }

  register(user: UserRegister) {
    return this.http.post(this.baseUrl + 'register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

}
