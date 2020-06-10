import { Injectable, LOCALE_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

import { defineLocale } from 'ngx-bootstrap/chronos';
import { frLocale } from 'ngx-bootstrap/locale';

export const defaultLanguage: any = 'fr';
defineLocale(defaultLanguage, frLocale);

@Injectable({
  providedIn: 'root'
})
export class LanguageService {

  private allLanguages = ['en', 'fr'];
  private currentLang = defaultLanguage;
  private baseUrl = environment.apiUrl + 'traduction';

  constructor(private http: HttpClient) { }

  getTranslate(lang: string): Observable<any> {
    return this.http.get<any>(this.baseUrl + '?lang=' + (typeof lang !== 'undefined' && lang ? lang : this.currentLang));
  }

  setCurrentLang(lang: string) {
    if (this.allLanguages.includes(lang)) {
      this.currentLang = lang;

      switch (lang) {
        case 'fr':
          defineLocale(this.currentLang, frLocale);
          break;
        case 'en':
          defineLocale(this.currentLang);
          break;
        default:
          defineLocale(defaultLanguage, frLocale);
          break;
      }
    }
  }

  getCurrentLang() {
    return this.currentLang;
  }
}
