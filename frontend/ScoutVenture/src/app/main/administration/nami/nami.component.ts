import { Component } from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { BadgeModule } from 'primeng/badge';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { CarouselModule } from 'primeng/carousel';
import { InputTextModule } from 'primeng/inputtext';
import { MeterGroupModule } from 'primeng/metergroup';
import { SkeletonModule } from 'primeng/skeleton';
import { TableModule } from 'primeng/table';
import { ErrorWrapperComponent } from '../../../shared/form/error-wrapper/error-wrapper.component';
import { Validators } from '../../../shared/form/Validators';
import { NamiOverviewComponent } from './overview/nami-overview.component';

@Component({
  selector: 'sv-nami',
  imports: [
    TableModule,
    CommonModule,
    ButtonModule,
    BadgeModule,
    MeterGroupModule,
    CardModule,
    CarouselModule,
    ErrorWrapperComponent,
    InputTextModule,
    SkeletonModule,
    ReactiveFormsModule,
    NamiOverviewComponent,
  ],
  templateUrl: './nami.component.html',
  styleUrl: './nami.component.scss',
})
export class NamiComponent {
  protected importForm = new FormGroup({
    groupId: new FormControl<string>('', {
      validators: [Validators.required],
      updateOn: 'blur',
    }),
    memberId: new FormControl<string>('', {
      validators: [Validators.required],
      updateOn: 'blur',
    }),
    password: new FormControl<string>('', {
      validators: [Validators.required],
      updateOn: 'blur',
    }),
  });

  protected import() {
    if (this.importForm.valid) {
    } else {
      this.importForm.markAsDirty();
      this.importForm.controls.groupId.markAsDirty();
      this.importForm.controls.memberId.markAsDirty();
      this.importForm.controls.password.markAsDirty();
    }
  }
}
