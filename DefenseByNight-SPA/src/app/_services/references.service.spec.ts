/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ReferencesService } from './references.service';

describe('Service: References', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReferencesService]
    });
  });

  it('should ...', inject([ReferencesService], (service: ReferencesService) => {
    expect(service).toBeTruthy();
  }));
});
