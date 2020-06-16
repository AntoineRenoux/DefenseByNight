import { NgModule } from '@angular/core';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

import { LanguageService, defaultLanguage, HttpLoaderFactory } from '../_services/language.service';
import { NavComponent } from './nav/nav.component';
import { NavigationPileDirective } from '../_directives/navigation-pile.directive';
import { BannerComponent } from './banner/banner.component';

@NgModule({
    declarations: [
        NavComponent,
        BannerComponent,
        NavigationPileDirective
    ],
    imports: [
        RouterModule,
        CommonModule,
        BrowserModule,
        BsDropdownModule.forRoot(),
        TranslateModule.forRoot({
            loader: { provide: TranslateLoader, useFactory: HttpLoaderFactory, deps: [LanguageService] },
            defaultLanguage
        })
    ],
    exports: [NavComponent, BannerComponent]
})
export class NavModule { }
