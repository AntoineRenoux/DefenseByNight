import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

import { UserLogin } from '../_models/userLogin';
import { User } from '../_models/user';
import { UserRegister } from '../_models/userRegister';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })};

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;

  constructor(private http: HttpClient) { }

  login(model: UserLogin) {
    return this.http.post(this.baseUrl + 'login', model, httpOptions)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            localStorage.setItem('user', JSON.stringify(user.appUser));
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            this.currentUser = user.appUser;
          }
        })
      );
  }

  register(user: UserRegister) {
    return this.http.post(this.baseUrl + 'register', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

}
