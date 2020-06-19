import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { map } from 'rxjs/operators';
import { Health } from '../_models/health';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl + 'user/';
  currentUser: User = null;

  constructor(private http: HttpClient) { }

  editUser(model: User) {
    return this.http.post(this.baseUrl + 'edituser', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            this.currentUser = user;
          }
        })
      );
  }

  editHealth(model: Health) {
    return this.http.post(this.baseUrl + this.currentUser.id + '/health', model)
      .pipe(
        map((response: any) => {
          const health = response;
          if (health) {
            this.currentUser.health = health;
          }
        })
      );
  }

}
