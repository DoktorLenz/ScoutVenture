import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AutoFocusModule } from 'primeng/autofocus';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { FloatLabelModule } from 'primeng/floatlabel';
import { FluidModule } from 'primeng/fluid';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ErrorWrapperComponent } from '../../shared/form/error-wrapper/error-wrapper.component';
import { Validators } from '../../shared/form/Validators';

@Component({
  selector: 'sv-forgot-password',
  imports: [
    CardModule,
    ButtonModule,
    InputTextModule,
    FloatLabelModule,
    FluidModule,
    RouterLink,
    PasswordModule,
    AutoFocusModule,
    ReactiveFormsModule,
    ErrorWrapperComponent,
    CommonModule,
  ],
  templateUrl: './forgot-password.component.html',
  styleUrl: './forgot-password.component.scss',
})
export class ForgotPasswordComponent {
  protected forgotPasswordForm = new FormGroup({
    email: new FormControl<string>('', {
      validators: [Validators.required],
      updateOn: 'blur',
    }),
  });

  protected forgotPasswordSent = false;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {}

  protected onSubmit() {
    if (this.forgotPasswordForm.valid) {
      this.http
        .post('/api/forgotPassword', this.forgotPasswordForm.value)
        .subscribe({
          complete: () => {
            this.forgotPasswordSent = true;
          },
        });
    } else {
      this.forgotPasswordForm.markAsDirty();
      this.forgotPasswordForm.controls.email.markAsDirty();
    }
  }
}
