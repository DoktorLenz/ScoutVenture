import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
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
  selector: 'sv-reset-password',
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
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.scss',
})
export class ResetPasswordComponent implements OnInit {
  @Input()
  email!: string;

  @Input()
  resetCode!: string;

  protected resetPasswordForm = new FormGroup(
    {
      email: new FormControl<string>(''),
      resetCode: new FormControl<string>(''),
      newPassword: new FormControl<string>('', {
        validators: [Validators.minLength(8)],
        updateOn: 'change',
      }),
      confirmPassword: new FormControl<string>(''),
    },
    {
      validators: [Validators.passwordMatch('newPassword', 'confirmPassword')],
      updateOn: 'change',
    }
  );

  protected resetPasswordFinished = false;
  protected resetCodeInvalid = false;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {}

  public ngOnInit(): void {
    if (this.email && this.resetCode) {
      this.resetPasswordForm.patchValue({
        email: this.email,
        resetCode: this.resetCode,
      });
    } else {
      this.router.navigateByUrl('/auth/forgotPassword');
    }
  }

  protected onSubmit() {
    if (this.resetPasswordForm.valid) {
      this.http
        .post('/api/resetPassword', this.resetPasswordForm.value)
        .subscribe({
          next: () => {
            this.resetPasswordFinished = true;
          },
          error: (error) => {
            this.resetCodeInvalid = true;
          },
        });
    } else {
      this.resetPasswordForm.markAsDirty();
      this.resetPasswordForm.controls.newPassword.markAsDirty();
      this.resetPasswordForm.controls.confirmPassword.markAsDirty();
    }
  }
}
