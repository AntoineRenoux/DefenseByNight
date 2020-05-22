import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl + 'user/';
  currentUser: User = null;

  constructor(private http: HttpClient) { }

  getUser(id: string) {
    return this.http.get(this.baseUrl + id);
  }

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

}
