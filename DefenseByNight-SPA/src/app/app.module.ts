import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { RouterModule } from '@angular/router';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { AppComponent } from './app.component';
import { AuthService } from './_services/auth.service';
import { RulesComponent } from './rules/rules.component';
import { CharacterComponent } from './character/character.component';
import { HomeComponent } from './home/home.component';
import { appRoutes } from './routes';

import { ToastrModule } from 'ngx-toastr';
import { ToasterService } from './_services/toaster.service';
import { NavComponent } from './nav/nav.component';
import { RegisterComponent } from './nav/register/register.component';
import { LanguageService } from './_services/language.service';
import { Observable, from } from 'rxjs';

export function tokenGetter() {
   return localStorage.getItem('token');
}

export class TranslationLoader implements TranslateLoader {
   constructor(private langService: LanguageService) {}

   getTranslation(lang: string): Observable<any> {
      return this.langService.getTranslate(lang);
   }
}

export function HttpLoaderFactory(langService: LanguageService) {
   return new TranslationLoader(langService);
}

@NgModule({
   declarations: [
      AppComponent,
      RulesComponent,
      CharacterComponent,
      HomeComponent,
      NavComponent,
      RegisterComponent
   ],
   imports: [
      BrowserModule,
      FormsModule,
      TooltipModule.forRoot(),
      HttpClientModule,
      BrowserAnimationsModule,
      ReactiveFormsModule,
      BsDropdownModule.forRoot(),
      JwtModule.forRoot({
         config: {
            tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         },
      }),
      RouterModule.forRoot(appRoutes),
      ToastrModule.forRoot({
         timeOut: 10000,
         positionClass: 'toast-bottom-right',
         preventDuplicates: true,
      }),
      TranslateModule.forRoot({
         loader: {provide: TranslateLoader, useFactory: HttpLoaderFactory, deps: [LanguageService]},
         defaultLanguage: 'fr'
      })
   ],
   providers: [
      AuthService,
      ToasterService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
