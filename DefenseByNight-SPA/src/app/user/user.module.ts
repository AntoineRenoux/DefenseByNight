import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { RegisterComponent } from './register/register.component';
import { ConnexionComponent } from './connexion/connexion.component';
import { UserRoutingModule } from './user-routing.module';
import { LanguageService, defaultLanguage } from '../_services/language.service';
import { HttpLoaderFactory } from '../app.module';


@NgModule({
    declarations: [
        RegisterComponent,
        ConnexionComponent
    ],
    imports: [
        CommonModule,
        UserRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        BsDatepickerModule.forRoot(),
        TranslateModule.forRoot({
            loader: { provide: TranslateLoader, useFactory: HttpLoaderFactory, deps: [LanguageService] },
            defaultLanguage
        })
    ],
    exports: []
})
export class UserModule { }
