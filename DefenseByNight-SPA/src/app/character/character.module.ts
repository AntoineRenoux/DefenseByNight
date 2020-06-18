import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';

import { HttpLoaderFactory, LanguageService, defaultLanguage } from '../_services/language.service';
import { CharacterRoutingModule } from './character-routing.module';

@NgModule({
  imports: [
    CommonModule,
    CharacterRoutingModule,
    TranslateModule.forRoot({
      loader: { provide: TranslateLoader, useFactory: HttpLoaderFactory, deps: [LanguageService] },
      defaultLanguage
  })
  ],
  declarations: [CreateComponent]
})
export class CharacterModule { }
