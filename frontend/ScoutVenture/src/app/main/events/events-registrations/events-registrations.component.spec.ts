import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EventsRegistrationsComponent } from './events-registrations.component';

describe('EventsRegistrationsComponent', () => {
  let component: EventsRegistrationsComponent;
  let fixture: ComponentFixture<EventsRegistrationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EventsRegistrationsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EventsRegistrationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
