import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppLoginButtonsComponent } from './app-login-buttons.component';

describe('AppLoginButtonsComponent', () => {
  let component: AppLoginButtonsComponent;
  let fixture: ComponentFixture<AppLoginButtonsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppLoginButtonsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppLoginButtonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
