import { Component, ViewChild } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AutoFocusModule } from 'primeng/autofocus';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { FloatLabelModule } from 'primeng/floatlabel';
import { FluidModule } from 'primeng/fluid';
import { InputTextModule } from 'primeng/inputtext';
import { Password, PasswordModule } from 'primeng/password';

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
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  protected registerForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
    repeatPassword: new FormControl(''),
  });

  @ViewChild('passwordInput') protected passwordInput!: Password;
  @ViewChild('repeatPasswordInput') protected repeatPasswordInput!: Password;

  // protected focusPassword() {
  //   this.passwordInput.el.nativeElement.querySelector('input').focus();
  // }

  // protected focusRepeatPassword() {
  //   this.repeatPasswordInput.el.nativeElement.querySelector('input').focus();
  // }

  protected onSubmit() {}
}
