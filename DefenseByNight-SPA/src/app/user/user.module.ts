import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TabsModule } from 'ngx-bootstrap/tabs';

import { RegisterComponent } from './register/register.component';
import { ConnexionComponent } from './connexion/connexion.component';
import { UserRoutingModule } from './user-routing.module';
import { LanguageService, defaultLanguage } from '../_services/language.service';
import { HttpLoaderFactory } from '../app.module';
import { AccountComponent } from './account/account.component';

import { FontAwesomeModule, FaIconLibrary  } from '@fortawesome/angular-fontawesome';
import { faAddressCard, faAt, faMobileAlt, faBirthdayCake, faWifi, faUserCircle } from '@fortawesome/free-solid-svg-icons';


@NgModule({
    declarations: [
        RegisterComponent,
        ConnexionComponent,
        AccountComponent,
    ],
    imports: [
        CommonModule,
        UserRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        BsDatepickerModule.forRoot(),
        TabsModule.forRoot(),
        FontAwesomeModule,
        TranslateModule.forRoot({
            loader: { provide: TranslateLoader, useFactory: HttpLoaderFactory, deps: [LanguageService] },
            defaultLanguage
        })
    ],
    exports: []
})
export class UserModule {
    constructor(library: FaIconLibrary) {
        library.addIcons(faAddressCard);
        library.addIcons(faAt);
        library.addIcons(faMobileAlt);
        library.addIcons(faBirthdayCake);
        library.addIcons(faWifi);
        library.addIcons(faUserCircle);
    }
}
