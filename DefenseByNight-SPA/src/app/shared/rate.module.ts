import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RateComponent } from './rate/rate.component';

import { FaIconLibrary, FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faCircle as fasCircle} from '@fortawesome/free-solid-svg-icons';
import { faCircle as farCircle} from '@fortawesome/free-regular-svg-icons';

@NgModule({
    imports: [CommonModule, FontAwesomeModule],
    declarations: [RateComponent],
    exports: [RateComponent]
  })
  export class RateModule {
    constructor(library: FaIconLibrary) {
        library.addIcons(fasCircle);
        library.addIcons(farCircle);
    }
  }
