import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppProductFiltersComponent } from './app-product-filters.component';

describe('AppProductFiltersComponent', () => {
  let component: AppProductFiltersComponent;
  let fixture: ComponentFixture<AppProductFiltersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppProductFiltersComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppProductFiltersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
