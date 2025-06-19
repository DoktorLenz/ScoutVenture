import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { FluidModule } from 'primeng/fluid';

@Component({
  selector: 'sv-confirm-email',
  imports: [CardModule, ButtonModule, FluidModule, RouterLink],
  templateUrl: './confirm-email.component.html',
  styleUrl: './confirm-email.component.scss',
})
export class ConfirmEmailComponent implements OnInit {
  @Input()
  userId!: string;

  @Input()
  code!: string;

  protected confirmationSuccess: boolean | null = null;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {}

  public ngOnInit() {
    if (this.userId && this.code) {
      this.http
        .get('/api/confirmEmail', {
          params: { userId: this.userId, code: this.code },
          responseType: 'text',
        })
        .subscribe({
          next: (val) => {
            this.confirmationSuccess = true;
          },
          error: (err) => {
            this.confirmationSuccess = false;
          },
        });
    }
  }

  protected redirectLogin() {
    this.router.navigateByUrl('/auth/login');
  }
}
