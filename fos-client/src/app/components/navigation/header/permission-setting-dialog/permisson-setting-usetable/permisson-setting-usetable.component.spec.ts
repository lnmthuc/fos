import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PermissonSettingUsetableComponent } from './permisson-setting-usetable.component';

describe('PermissonSettingUsetableComponent', () => {
  let component: PermissonSettingUsetableComponent;
  let fixture: ComponentFixture<PermissonSettingUsetableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PermissonSettingUsetableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PermissonSettingUsetableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
