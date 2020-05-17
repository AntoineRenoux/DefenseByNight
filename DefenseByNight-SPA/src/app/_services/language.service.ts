import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  baseUrl = environment.apiUrl + 'traduction';

  constructor(private http: HttpClient) { }

  getTranslate(lang: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + '?lang=' + lang);
  }
}
