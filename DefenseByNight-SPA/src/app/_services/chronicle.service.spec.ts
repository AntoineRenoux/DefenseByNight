/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ChronicleService } from './chronicle.service';

describe('Service: Chronicle', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ChronicleService]
    });
  });

  it('should ...', inject([ChronicleService], (service: ChronicleService) => {
    expect(service).toBeTruthy();
  }));
});
