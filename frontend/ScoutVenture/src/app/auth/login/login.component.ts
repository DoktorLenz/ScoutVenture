import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { FloatLabelModule } from 'primeng/floatlabel';
import { FluidModule } from 'primeng/fluid';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'sv-login',
  imports: [
    CardModule,
    ButtonModule,
    InputTextModule,
    FloatLabelModule,
    FluidModule,
    RouterLink,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {}
