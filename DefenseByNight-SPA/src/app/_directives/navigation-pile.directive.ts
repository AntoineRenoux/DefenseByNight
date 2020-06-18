import { Directive, ElementRef, HostListener, Renderer2 } from '@angular/core';

@Directive({
  // tslint:disable-next-line: directive-selector
  selector: '[dbnNavigationPile]'
})
export class NavigationPileDirective {

  constructor(private elementRef: ElementRef, private renderer: Renderer2) { }

  @HostListener('click') onClick() {
    const elem = document.getElementsByClassName('navbox active');
    if (elem.length > 0) {
      // tslint:disable-next-line: prefer-for-of
      for (let index = 0; index < elem.length; index++) {
        const element = elem[index];
        element.classList.remove('active');
      }
    }
    this.renderer.addClass(this.elementRef.nativeElement, 'active');
  }
}
