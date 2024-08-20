import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppProductItemComponent } from './app-product-item.component';

describe('AppProductItemComponent', () => {
  let component: AppProductItemComponent;
  let fixture: ComponentFixture<AppProductItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppProductItemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppProductItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
