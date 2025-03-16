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
  selector: 'sv-login',
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
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  protected loginForm = new FormGroup({
    email: new FormControl<string>('', {
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
    private readonly router: Router
  ) {}

  protected onSubmit() {
    if (this.loginForm.valid) {
      this.http
        .post('/api/login?useCookies=true', this.loginForm.value)
        .subscribe({
          next: () => {
            this.router.navigate(['/']);
          },
          error: () => {
            this.loginForm.setErrors({ invalidCredentials: true });
            this.loginForm.controls.password.setErrors({
              invalidCredentials: true,
            });
            this.loginForm.controls.email.setErrors({
              invalidCredentials: true,
            });
          },
        });
    } else {
      this.loginForm.markAsDirty();
      this.loginForm.controls.email.markAsDirty();
      this.loginForm.controls.password.markAsDirty();
    }
  }
}
