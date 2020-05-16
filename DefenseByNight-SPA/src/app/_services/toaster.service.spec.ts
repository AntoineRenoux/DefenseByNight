/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ToasterService } from './toaster.service';

describe('Service: Toaster', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ToasterService]
    });
  });

  it('should ...', inject([ToasterService], (service: ToasterService) => {
    expect(service).toBeTruthy();
  }));
});
