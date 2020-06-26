/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ClanService } from './clan.service';

describe('Service: Clan', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClanService]
    });
  });

  it('should ...', inject([ClanService], (service: ClanService) => {
    expect(service).toBeTruthy();
  }));
});
