import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Discipline } from '../_models/discipline';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DisciplinesService {

  constructor(private http: HttpClient) { }

  baseUrl = environment.apiUrl + 'discipline';

  getDisciplines(): Observable<Discipline[]> {
    return this.http.get<Discipline[]>(this.baseUrl);
  }

}
