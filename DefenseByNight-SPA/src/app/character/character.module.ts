import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateComponent } from './create/create.component';
import { CharacterRoutingModule } from './character-routing.module';

@NgModule({
  imports: [
    CommonModule,
    CharacterRoutingModule
  ],
  declarations: [CreateComponent]
})
export class CharacterModule { }
