import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppOnlineUsersComponent } from './app-online-users.component';

describe('AppOnlineUsersComponent', () => {
  let component: AppOnlineUsersComponent;
  let fixture: ComponentFixture<AppOnlineUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppOnlineUsersComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppOnlineUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
