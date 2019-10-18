import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PermissionSearchMutipeopleComponent } from './permission-search-mutipeople.component';

describe('PermissionSearchMutipeopleComponent', () => {
  let component: PermissionSearchMutipeopleComponent;
  let fixture: ComponentFixture<PermissionSearchMutipeopleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PermissionSearchMutipeopleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PermissionSearchMutipeopleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
