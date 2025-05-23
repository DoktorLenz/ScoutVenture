import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NamiComponent } from './nami.component';

describe('NamiComponent', () => {
  let component: NamiComponent;
  let fixture: ComponentFixture<NamiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NamiComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NamiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
