import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NamiImportComponent } from './nami-import.component';

describe('NamiImportComponent', () => {
  let component: NamiImportComponent;
  let fixture: ComponentFixture<NamiImportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NamiImportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NamiImportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
