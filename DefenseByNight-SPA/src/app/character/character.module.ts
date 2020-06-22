import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HttpLoaderFactory, LanguageService, defaultLanguage } from '../_services/language.service';
import { CharacterRoutingModule } from './character-routing.module';
import { HelpCreationCharacterDirective } from '../_directives/help-creation-character.directive';

@NgModule({
  imports: [
    CommonModule,
    CharacterRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule.forRoot({
      loader: { provide: TranslateLoader, useFactory: HttpLoaderFactory, deps: [LanguageService] },
      defaultLanguage
  })
  ],
  declarations: [CreateComponent, HelpCreationCharacterDirective]
})
export class CharacterModule { }
