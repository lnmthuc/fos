import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EventDialogParticipantpickerComponent } from './event-dialog-participantpicker.component';

describe('EventDialogParticipantpickerComponent', () => {
  let component: EventDialogParticipantpickerComponent;
  let fixture: ComponentFixture<EventDialogParticipantpickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventDialogParticipantpickerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventDialogParticipantpickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
