import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { FluidModule } from 'primeng/fluid';
import { Validators } from '../../shared/form/Validators';

@Component({
  selector: 'sv-reset-password',
  imports: [CardModule, ButtonModule, FluidModule, RouterLink, CommonModule],
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

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {}
  ngOnInit(): void {
    if (this.email && this.resetCode) {
      this.resetPasswordForm.patchValue({
        email: this.email,
        resetCode: this.resetCode,
      });
    } else {
      this.router.navigateByUrl('/auth/forgotPassword');
    }
  }
}
