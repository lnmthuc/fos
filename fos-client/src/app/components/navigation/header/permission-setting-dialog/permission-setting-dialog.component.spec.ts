import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PermissionSettingDialogComponent } from './permission-setting-dialog.component';

describe('PermissionSettingDialogComponent', () => {
  let component: PermissionSettingDialogComponent;
  let fixture: ComponentFixture<PermissionSettingDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PermissionSettingDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PermissionSettingDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
