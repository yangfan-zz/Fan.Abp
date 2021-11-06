import { TestBed } from '@angular/core/testing';

import { DomainCqrsService } from './domain-cqrs.service';

describe('DomainCqrsService', () => {
  let service: DomainCqrsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DomainCqrsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
