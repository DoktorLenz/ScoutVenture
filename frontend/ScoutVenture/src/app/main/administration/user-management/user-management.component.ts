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
import { v4 } from 'uuid'; // Importing v4 from uuid package for generating unique IDs
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
          // this.users.set(data);
          this.users.set([
            {
              id: v4(),
              firstName: 'John',
              lastName: 'Doe',
              email: 'jdoe@family.org',
            },
            {
              id: v4(),
              firstName: 'Jane',
              lastName: 'Doe',
              email: 'jjdoe7466@hotmail.com',
            },
            {
              id: v4(),
              firstName: 'Alice',
              lastName: 'Smith',
              email: 'alice.smith@dhc.gov.us',
            },
            {
              id: v4(),
              firstName: 'Bob',
              lastName: 'Johnson',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Charlie',
              lastName: 'Brown',
              email: 'charlie.brown76@yahoo.com',
            },
            {
              id: v4(),
              firstName: 'Diana Marie Magdalene',
              lastName: 'Prince',
              email: 'dianamariemagdalene.prince@gmail.com',
            },
            {
              id: v4(),
              firstName: 'Ethan',
              lastName: 'Hunt',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Fiona',
              lastName: 'Green',
              email: '',
            },
            {
              id: v4(),
              firstName: 'George',
              lastName: 'White',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Hannah',
              lastName: 'Black',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Ian',
              lastName: 'Gray',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Jack',
              lastName: 'Blue',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Kathy',
              lastName: 'Red',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Liam',
              lastName: 'Yellow',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Mia',
              lastName: 'Purple',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Noah',
              lastName: 'Orange',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Olivia',
              lastName: 'Pink',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Paul',
              lastName: 'Cyan',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Quinn',
              lastName: 'Magenta',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Rita',
              lastName: 'Brown',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Sam',
              lastName: 'White',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Tina',
              lastName: 'Black',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Ursula',
              lastName: 'Gray',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Victor',
              lastName: 'Blue',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Wendy',
              lastName: 'Red',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Xander',
              lastName: 'Yellow',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Yara',
              lastName: 'Purple',
              email: '',
            },
            {
              id: v4(),
              firstName: 'Zane',
              lastName: 'Orange',
              email: '',
            },
          ]);
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
