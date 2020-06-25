import { Directive, HostListener, Input, Renderer2 } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Directive({
  // tslint:disable-next-line: directive-selector
  selector: '[dbnHelpCreationCharacter]'
})
export class HelpCreationCharacterDirective {

  @Input() headerText: string;
  @Input() bodyText: string;
  defaultHeaderText = 'CREATION_CHARACTER_LBL_HELP_HEADER';
  defaultBodyText = 'CREATION_CHARACTER_LBL_HELP_TEXT_DEFAULT';

  constructor(private renderer: Renderer2,
              private translateService: TranslateService) { }

  @HostListener('mouseenter') onMouseEnter() {
    this.editHeader(this.headerText);
    this.editBody(this.bodyText);
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.editHeader();
    this.editBody();
  }

  editHeader(text?: string) {
    const header = this.renderer.selectRootElement('.info-card .card-title', true);

    this.translateService.get(text != null ? text : this.defaultHeaderText).subscribe(res => {
      this.renderer.setProperty(header, 'textContent', res);
    });
  }

  editBody(text?: string) {
    const body = this.renderer.selectRootElement('.info-card .card-text', true);

    this.translateService.get(text != null ? text : this.defaultBodyText).subscribe(res => {
      this.renderer.setProperty(body, 'textContent', res);
    });
  }
}
