import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Affilate } from '../_models/affiliate';

@Injectable({
  providedIn: 'root'
})
export class AffiliateService {

  private baseUrl = environment.apiUrl + 'affiliation';

  constructor(private http: HttpClient) { }

  getAllAffiliations(): Observable<Affilate[]> {
    return this.http.get<Affilate[]>(this.baseUrl);
  }

}
