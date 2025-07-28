import { HttpClient } from '@angular/common/http';
import { Component, computed, signal, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { AvatarModule } from 'primeng/avatar';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { ScrollerModule } from 'primeng/scroller';
import { Table, TableModule } from 'primeng/table';
import { User } from './models/user';

@Component({
  selector: 'sv-user-management',
  imports: [
    CardModule,
    TableModule,
    ButtonModule,
    ScrollerModule,
    AvatarModule,
    AutoCompleteModule,
    IconFieldModule,
    InputIconModule,
    InputTextModule,
  ],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.scss',
})
export class UserManagementComponent {
  @ViewChild('table') table!: Table;

  protected suggestions = computed(() =>
    this.users().map((user) => user.firstName + ' ' + user.lastName)
  );
  protected users = signal<User[]>([]);
  protected loading = signal(true);
  protected selectedUser: User | null = null;

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {
    this.fetchUsers();
  }

  private fetchUsers() {
    this.http
      .get<User[]>('/api/administration/user-management/users')
      .subscribe({
        next: (data) => {
          this.users.set(data);
          this.loading.set(false);
        },
        error: (err) => {
          console.error('Error fetching users:', err);
          this.loading.set(false);
        },
      });
  }

  protected search(event: Event): void {
    var t = event.target as HTMLInputElement;
    this.table.filterGlobal(t.value, 'contains');
  }

  protected gotoUserDetails(user: User | User[] | undefined): void {
    if (user && !Array.isArray(user)) {
      this.router.navigate([`/administration/user-management/${user.id}`]);
    }
  }
}
