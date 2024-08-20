import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppShortUserProfileComponent } from './app-short-user-profile.component';

describe('AppShortUserProfileComponent', () => {
  let component: AppShortUserProfileComponent;
  let fixture: ComponentFixture<AppShortUserProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppShortUserProfileComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppShortUserProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
