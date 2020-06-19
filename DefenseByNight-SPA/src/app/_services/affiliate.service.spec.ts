/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AffiliateService } from './affiliate.service';

describe('Service: Affiliate', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AffiliateService]
    });
  });

  it('should ...', inject([AffiliateService], (service: AffiliateService) => {
    expect(service).toBeTruthy();
  }));
});
