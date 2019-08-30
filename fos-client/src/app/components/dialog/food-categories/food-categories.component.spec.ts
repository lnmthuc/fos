import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodCategoriesComponent } from './food-categories.component';

describe('FoodCategoriesComponent', () => {
  let component: FoodCategoriesComponent;
  let fixture: ComponentFixture<FoodCategoriesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FoodCategoriesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FoodCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
