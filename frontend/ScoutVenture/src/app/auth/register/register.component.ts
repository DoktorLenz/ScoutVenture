import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
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
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent {
  private formBuilder = inject(FormBuilder);
  protected registerForm = this.formBuilder.group({
    email: new FormControl<string>('', {
      validators: [Validators.required, Validators.email],
      updateOn: 'blur',
    }),
    password: new FormControl<string>('', {
      validators: [Validators.required, Validators.minLength(8)],
      updateOn: 'blur',
    }),
    repeatPassword: new FormControl<string>('', {
      validators: [Validators.required],
      updateOn: 'blur',
    }),
  });

  protected onSubmit() {
    console.warn(this.registerForm.controls.email.errors);
  }
}
