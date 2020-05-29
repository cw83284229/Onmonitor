import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OnMonitorComponent } from './on-monitor.component';

describe('OnMonitorComponent', () => {
  let component: OnMonitorComponent;
  let fixture: ComponentFixture<OnMonitorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [OnMonitorComponent],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OnMonitorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
