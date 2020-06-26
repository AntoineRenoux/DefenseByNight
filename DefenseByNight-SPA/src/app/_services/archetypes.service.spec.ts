/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ArchetypesService } from './archetypes.service';

describe('Service: Archetypes', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ArchetypesService]
    });
  });

  it('should ...', inject([ArchetypesService], (service: ArchetypesService) => {
    expect(service).toBeTruthy();
  }));
});
