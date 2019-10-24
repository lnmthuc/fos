import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DelegateSettingDialogComponent } from './delegate-setting-dialog.component';

describe('DelegateSettingDialogComponent', () => {
  let component: DelegateSettingDialogComponent;
  let fixture: ComponentFixture<DelegateSettingDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DelegateSettingDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DelegateSettingDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
