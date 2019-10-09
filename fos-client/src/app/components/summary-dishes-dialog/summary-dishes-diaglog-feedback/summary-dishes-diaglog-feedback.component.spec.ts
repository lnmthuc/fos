import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SummaryDishesDiaglogFeedbackComponent } from './summary-dishes-diaglog-feedback.component';

describe('SummaryDishesDiaglogFeedbackComponent', () => {
  let component: SummaryDishesDiaglogFeedbackComponent;
  let fixture: ComponentFixture<SummaryDishesDiaglogFeedbackComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SummaryDishesDiaglogFeedbackComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SummaryDishesDiaglogFeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
