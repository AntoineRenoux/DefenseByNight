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
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { TranslateLoader } from '@ngx-translate/core';
import { ToastrModule } from 'ngx-toastr';

import { AppComponent } from './app.component';
import { LanguageService, defaultLanguage, HttpLoaderFactory } from './_services/language.service';
import { AppRoutingModule } from './app-routing.module';
import { NavModule } from './navbar/nav.module';

registerLocaleData(localeFr, 'fr-FR', localeFrExtra);

export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent
   ],
   imports: [
      NavModule,
      BrowserModule,
      FormsModule,
      AppRoutingModule,
      TooltipModule.forRoot(),
      HttpClientModule,
      BrowserAnimationsModule,
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
