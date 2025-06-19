import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, effect } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ProblemDetails } from '../../../../shared/error/ProblemDetails';
import { ErrorSummaryComponent } from '../../../../shared/form/error-summary/error-summary.component';
import { ErrorWrapperComponent } from '../../../../shared/form/error-wrapper/error-wrapper.component';
import { NamiService } from '../nami.service';

@Component({
  selector: 'sv-nami-import',
  imports: [
    ErrorWrapperComponent,
    ErrorSummaryComponent,
    CardModule,
    InputTextModule,
    PasswordModule,
    ReactiveFormsModule,
    ButtonModule,
  ],
  templateUrl: './nami-import.component.html',
  styleUrl: './nami-import.component.scss',
})
export class NamiImportComponent {
  protected importForm = new FormGroup({
    groupingId: new FormControl<string>('', {
      validators: [Validators.required],
      updateOn: 'change',
    }),
    memberId: new FormControl<string>('', {
      validators: [Validators.required],
      updateOn: 'change',
    }),
    password: new FormControl<string>('', {
      validators: [Validators.required],
      updateOn: 'change',
    }),
  });

  constructor(
    private readonly http: HttpClient,
    private readonly namiService: NamiService
  ) {
    effect(() => {
      if (this.namiService.importPending()) {
        this.importForm.disable();
      } else {
        this.importForm.enable();
      }
    });
  }

  protected import() {
    if (this.importForm.valid) {
      this.namiService.importPending.set(true);
      this.http
        .post('/api/administration/nami/import', this.importForm.value)
        .subscribe({
          next: () => {
            this.importForm.reset();
            this.importForm.markAsPristine();
            this.namiService.importPending.set(false);
          },
          error: (response: HttpErrorResponse) => {
            const error = response.error as ProblemDetails;
            this.importForm.setErrors({
              custom: error.title || 'Ein unbekannter Fehler ist aufgetreten.',
            });
            this.namiService.importPending.set(false);
          },
        });
    } else {
      this.importForm.markAsDirty();
      this.importForm.controls.groupingId.markAsDirty();
      this.importForm.controls.memberId.markAsDirty();
      this.importForm.controls.password.markAsDirty();
    }
  }
}
