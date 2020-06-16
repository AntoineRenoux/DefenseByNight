import { Directive, ElementRef, HostListener, Renderer2 } from '@angular/core';

@Directive({
  // tslint:disable-next-line: directive-selector
  selector: '[dbnNavigationPile]'
})
export class NavigationPileDirective {

  constructor(private elementRef: ElementRef, private renderer: Renderer2) { }

  @HostListener('click') onClick() {
    const toto = document.getElementsByClassName('navbox active');
    if (toto.length > 0) {
      for (let index = 0; index < toto.length; index++) {
        const element = toto[index];
        element.classList.remove('active');
      }
    }
    this.renderer.addClass(this.elementRef.nativeElement, 'active');
  }
}
