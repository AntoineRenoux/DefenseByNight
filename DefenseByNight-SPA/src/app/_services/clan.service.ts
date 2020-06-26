import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Clan } from '../_models/clan';

@Injectable({
  providedIn: 'root'
})
export class ClanService {

  baseUrl = environment.apiUrl + 'clan';

  constructor(private http: HttpClient) { }

  getAllClans(): Observable<Clan[]> {
    return this.http.get<Clan[]>(this.baseUrl);
  }
}
