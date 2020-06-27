import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  // tslint:disable-next-line: component-selector
  selector: 'dbn-rate',
  templateUrl: './rate.component.html',
  styleUrls: ['./rate.component.css']
})
export class RateComponent implements OnInit {

  @Input() currentRate: number;
  @Input() nbCircleMax: number;
  @Input() maxRate?: number;

  @Output() newRate = new EventEmitter<number>();

  constructor() { }

  ngOnInit() {
  }

  counter(i: number) {
    return new Array(i);
  }

  rate(rate: number) {
    if (this.maxRate == null || rate <= this.maxRate) {
      this.currentRate = rate;
      this.newRate.emit(rate);
    }
  }
}
