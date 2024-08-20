import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppProductPricesComponent } from './app-product-prices.component';

describe('AppProductPricesComponent', () => {
  let component: AppProductPricesComponent;
  let fixture: ComponentFixture<AppProductPricesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppProductPricesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppProductPricesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
