import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppHeaderRouteComponentsComponent } from './app-header-route-components.component';

describe('AppHeaderRouteComponentsComponent', () => {
  let component: AppHeaderRouteComponentsComponent;
  let fixture: ComponentFixture<AppHeaderRouteComponentsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppHeaderRouteComponentsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppHeaderRouteComponentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
