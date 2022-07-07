import { TestBed } from '@angular/core/testing';

import { CamundabpmService } from './camundabpm.service';

describe('CamundabpmService', () => {
  let service: CamundabpmService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CamundabpmService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
