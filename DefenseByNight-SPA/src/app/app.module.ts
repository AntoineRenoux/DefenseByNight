import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { HttpClientModule } from '@angular/common/http';
import { LOCALE_ID } from '@angular/core';
import { registerLocaleData } from '@angular/common';
import localeFr from '@angular/common/locales/fr';
import localeFrExtra from '@angular/common/locales/extra/fr';
import { JwtModule } from '@auth0/angular-jwt';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { TranslateLoader } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { NavComponent } from './navbar/nav/nav.component';
import { LanguageService, defaultLanguage } from './_services/language.service';
import { AppRoutingModule } from './app-routing.module';
import { NavigationPileDirective } from './_directives/navigation-pile.directive';

registerLocaleData(localeFr, 'fr-FR', localeFrExtra);

export function tokenGetter() {
   return localStorage.getItem('token');
}

export class TranslationLoader implements TranslateLoader {
   constructor(private langService: LanguageService) { }

   getTranslation(lang: string): Observable<any> {
       return this.langService.getTranslate(lang);
   }

   getCurrentLang(): any {
      return this.langService.getCurrentLang();
   }
}

export function HttpLoaderFactory(langService: LanguageService) {
   return new TranslationLoader(langService);
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      NavigationPileDirective
   ],
   imports: [
      BrowserModule,
      FormsModule,
      AppRoutingModule,
      TooltipModule.forRoot(),
      HttpClientModule,
      BrowserAnimationsModule,
      BsDropdownModule.forRoot(),
      JwtModule.forRoot({
         config: {
            tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         },
      }),
      ToastrModule.forRoot({
         timeOut: 3000,
         positionClass: 'toast-bottom-right',
         preventDuplicates: true,
      }),
      TranslateModule.forRoot({
         loader: { provide: TranslateLoader, useFactory: HttpLoaderFactory, deps: [LanguageService] },
         defaultLanguage
     }),
   ],
   providers: [
      {
         provide: LOCALE_ID, useValue: 'fr-FR',
      }],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
