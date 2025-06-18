import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
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
import { ErrorWrapperComponent } from '../../../../shared/form/error-wrapper/error-wrapper.component';
import { NamiService } from '../nami.service';

@Component({
  selector: 'sv-nami-import',
  imports: [
    ErrorWrapperComponent,
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

  constructor(
    private readonly http: HttpClient,
    private readonly namiService: NamiService
  ) {}

  protected import() {
    if (this.importForm.valid) {
      this.http
        .post('/api/administration/nami/import', this.importForm.value)
        .subscribe({
          next: () => {
            this.importForm.reset();
            this.importForm.markAsPristine();
          },
          error: (error) => {
            if (error.status === 400) {
              this.importForm.setErrors({ invalidCredentials: true });
            } else {
              this.importForm.setErrors({ unknownError: true });
            }
          },
          complete: () => {
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
