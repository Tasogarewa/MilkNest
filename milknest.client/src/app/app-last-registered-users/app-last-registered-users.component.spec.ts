import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppLastRegisteredUsersComponent } from './app-last-registered-users.component';

describe('AppLastRegisteredUsersComponent', () => {
  let component: AppLastRegisteredUsersComponent;
  let fixture: ComponentFixture<AppLastRegisteredUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppLastRegisteredUsersComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppLastRegisteredUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
