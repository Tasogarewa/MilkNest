import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppMainUserProfileComponent } from './app-main-user-profile.component';

describe('AppMainUserProfileComponent', () => {
  let component: AppMainUserProfileComponent;
  let fixture: ComponentFixture<AppMainUserProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppMainUserProfileComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppMainUserProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
