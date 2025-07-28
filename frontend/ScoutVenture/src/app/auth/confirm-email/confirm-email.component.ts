import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { FluidModule } from 'primeng/fluid';
import { InputTextModule } from 'primeng/inputtext';
import { ProblemDetails } from '../../shared/error/ProblemDetails';
import { Validators } from '../../shared/form/Validators';
import { ErrorSummaryComponent } from '../../shared/form/error-summary/error-summary.component';
import { ErrorWrapperComponent } from '../../shared/form/error-wrapper/error-wrapper.component';

@Component({
  selector: 'sv-confirm-email',
  imports: [
    CardModule,
    ButtonModule,
    FluidModule,
    RouterLink,
    ReactiveFormsModule,
    ErrorWrapperComponent,
    ErrorSummaryComponent,
    InputTextModule,
  ],
  templateUrl: './confirm-email.component.html',
  styleUrl: './confirm-email.component.scss',
})
export class ConfirmEmailComponent {
  @Input()
  public set userId(value: string) {
    this.emailConfirmationForm.controls.userId.setValue(value);
  }

  @Input()
  public set code(value: string) {
    this.emailConfirmationForm.controls.code.setValue(atob(value));
  }

  protected emailConfirmationForm = new FormGroup({
    userId: new FormControl<string>(this.userId, {}),
    code: new FormControl<string>(this.code, {}),
    firstName: new FormControl<string>('', {
      updateOn: 'change',
      validators: [Validators.required],
    }),
    lastName: new FormControl<string>('', {
      updateOn: 'change',
      validators: [Validators.required],
    }),
    phoneNumber: new FormControl<string>('', {
      updateOn: 'change',
      validators: [Validators.required, Validators.phoneNumber()],
    }),
  });

  protected confirmationSuccess: boolean | null = null;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {}

  protected onSubmit(): void {
    if (this.emailConfirmationForm.valid) {
      this.http
        .post('/api/auth/confirmEmail', this.emailConfirmationForm.value)
        .subscribe({
          next: () => {
            this.confirmationSuccess = true;
          },
          error: (response: HttpErrorResponse) => {
            const error = response.error as ProblemDetails;
            this.emailConfirmationForm.setErrors({
              custom: error.title || 'Ein unbekannter Fehler ist aufgetreten.',
            });
          },
        });
    } else {
      this.emailConfirmationForm.markAsDirty();
      this.emailConfirmationForm.controls.firstName.markAsDirty();
      this.emailConfirmationForm.controls.lastName.markAsDirty();
      this.emailConfirmationForm.controls.phoneNumber.markAsDirty();
    }
  }

  protected redirectLogin() {
    this.router.navigateByUrl('/auth/login');
  }
}
