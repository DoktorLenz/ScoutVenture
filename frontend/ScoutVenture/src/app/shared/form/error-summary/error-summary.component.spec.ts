import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ErrorSummaryComponent } from './error-summary.component';

describe('ErrorSummaryComponent', () => {
  let component: ErrorSummaryComponent;
  let fixture: ComponentFixture<ErrorSummaryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ErrorSummaryComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ErrorSummaryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
