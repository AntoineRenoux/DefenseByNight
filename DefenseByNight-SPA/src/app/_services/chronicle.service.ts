import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Chronicle } from '../_models/Chronicle';

@Injectable({
  providedIn: 'root'
})
export class ChronicleService {

  baseUrl = environment.apiUrl + 'chronicle';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Chronicle[]> {
    return this.http.get<Chronicle[]>(this.baseUrl);
  }

}
