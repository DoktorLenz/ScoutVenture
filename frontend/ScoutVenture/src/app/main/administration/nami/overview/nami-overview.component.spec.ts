import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NamiOverviewComponent } from './nami-overview.component';

describe('NamiOverviewComponent', () => {
  let component: NamiOverviewComponent;
  let fixture: ComponentFixture<NamiOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NamiOverviewComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NamiOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
