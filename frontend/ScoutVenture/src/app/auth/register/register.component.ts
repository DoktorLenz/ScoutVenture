import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormControl, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AutoFocusModule } from 'primeng/autofocus';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { DividerModule } from 'primeng/divider';
import { FloatLabelModule } from 'primeng/floatlabel';
import { FluidModule } from 'primeng/fluid';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { PasswordModule } from 'primeng/password';
import { PopoverModule } from 'primeng/popover';
import { TooltipModule } from 'primeng/tooltip';
import { ErrorWrapperComponent } from '../../shared/form/error-wrapper/error-wrapper.component';
import { Validators } from '../../shared/form/Validators';

@Component({
  selector: 'sv-register',
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
    DividerModule,
    MessageModule,
    TooltipModule,
    PopoverModule,
    ErrorWrapperComponent,
    CommonModule,
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  private formBuilder = inject(FormBuilder);

  protected registerForm = this.formBuilder.group(
    {
      email: new FormControl<string>('', {
        validators: [Validators.email()],
        updateOn: 'blur',
      }),
      password: new FormControl<string>('', {
        validators: [Validators.minLength(8)],
        updateOn: 'change',
      }),
      confirmPassword: new FormControl<string>(''),
    },
    {
      validators: [Validators.passwordMatch('password', 'confirmPassword')],
      updateOn: 'change',
    }
  );

  protected registrationFinished = false;

  constructor(private readonly http: HttpClient) {}

  protected onSubmit() {
    if (this.registerForm.valid) {
      this.http.post('/api/register', this.registerForm.value).subscribe({
        next: () => {
          this.registrationFinished = true;
        },
        error: (error) => {
          this.registerForm.controls.email.setErrors(error.error.errors);
        },
      });
    } else {
      this.registerForm.markAsDirty();
      this.registerForm.controls.email.markAsDirty();
      this.registerForm.controls.password.markAsDirty();
      this.registerForm.controls.confirmPassword.markAsDirty();
    }
  }
}
