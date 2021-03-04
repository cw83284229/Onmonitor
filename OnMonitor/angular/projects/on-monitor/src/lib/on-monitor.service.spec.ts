import { TestBed } from '@angular/core/testing';

import { OnMonitorService } from './on-monitor.service';

describe('OnMonitorService', () => {
  let service: OnMonitorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OnMonitorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
