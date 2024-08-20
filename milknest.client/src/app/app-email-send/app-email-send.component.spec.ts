import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppEmailSendComponent } from './app-email-send.component';

describe('AppEmailSendComponent', () => {
  let component: AppEmailSendComponent;
  let fixture: ComponentFixture<AppEmailSendComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppEmailSendComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppEmailSendComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
