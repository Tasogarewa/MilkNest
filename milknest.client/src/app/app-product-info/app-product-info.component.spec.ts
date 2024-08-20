import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppProductInfoComponent } from './app-product-info.component';

describe('AppProductInfoComponent', () => {
  let component: AppProductInfoComponent;
  let fixture: ComponentFixture<AppProductInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppProductInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppProductInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
