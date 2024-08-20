import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppProductShowcaseComponent } from './app-product-showcase.component';

describe('AppProductShowcaseComponent', () => {
  let component: AppProductShowcaseComponent;
  let fixture: ComponentFixture<AppProductShowcaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppProductShowcaseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppProductShowcaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
