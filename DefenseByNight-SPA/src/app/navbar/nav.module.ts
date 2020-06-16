import { NgModule } from '@angular/core';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

import { LanguageService, defaultLanguage } from '../_services/language.service';
import { HttpLoaderFactory } from '../app.module';
import { NavComponent } from './nav/nav.component';

@NgModule({
    declarations: [
        NavComponent
    ],
    imports: [
        CommonModule,
        BrowserModule,
        BsDatepickerModule.forRoot(),
        TranslateModule.forRoot({
            loader: { provide: TranslateLoader, useFactory: HttpLoaderFactory, deps: [LanguageService] },
            defaultLanguage
        })
    ],
    exports: []
})
export class NavModule { }
