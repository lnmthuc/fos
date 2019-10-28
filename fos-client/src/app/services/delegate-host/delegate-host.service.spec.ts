import { TestBed } from '@angular/core/testing';

import { DelegateHostService } from './delegate-host.service';

describe('DelegateHostService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DelegateHostService = TestBed.get(DelegateHostService);
    expect(service).toBeTruthy();
  });
});
