import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AutoFocusModule } from 'primeng/autofocus';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { FloatLabelModule } from 'primeng/floatlabel';
import { FluidModule } from 'primeng/fluid';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { ErrorWrapperComponent } from '../../shared/form/error-wrapper/error-wrapper.component';

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
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  protected loginForm = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });

  onSubmit() {
    console.warn(this.loginForm.value);
  }
}
