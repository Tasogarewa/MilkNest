import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppCommentSectionComponent } from './app-comment-section.component';

describe('AppCommentSectionComponent', () => {
  let component: AppCommentSectionComponent;
  let fixture: ComponentFixture<AppCommentSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppCommentSectionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppCommentSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
