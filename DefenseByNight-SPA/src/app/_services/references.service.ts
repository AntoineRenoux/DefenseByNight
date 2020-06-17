import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReferencesService {

  private baseUrl = environment.apiUrl + 'reference';

  constructor(private http: HttpClient) { }

  getBanner(): Observable<any> {
    return this.http.get<any>(this.baseUrl + '/getbanner');
  }

  getLogo(): Observable<any> {
    return this.http.get<any>(this.baseUrl + '/getlogo');
  }

}
