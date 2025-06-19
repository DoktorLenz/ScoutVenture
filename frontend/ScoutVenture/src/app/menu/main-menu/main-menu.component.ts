import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { AvatarModule } from 'primeng/avatar';
import { BadgeModule } from 'primeng/badge';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';

@Component({
  selector: 'sv-main-menu',
  imports: [MenuModule, BadgeModule, AvatarModule, ButtonModule, RouterModule],
  templateUrl: './main-menu.component.html',
  styleUrl: './main-menu.component.scss',
})
export class MainMenuComponent implements OnInit {
  @Output()
  public close = new EventEmitter<Event>();

  items: MenuItem[] | undefined;

  public ngOnInit() {
    this.items = [
      {
        label: 'Veranstaltungen',
        items: [
          {
            label: 'Übersicht',
            icon: 'pi pi-calendar',
            routerLink: '/events/overview',
          },
          {
            label: 'Meine Anmeldungen',
            icon: 'pi pi-receipt',
            routerLink: '/events/registrations',
          },
        ],
      },
      { separator: true },
      {
        label: 'Meine Personen',
        items: [
          {
            label: 'Max Mustermann',
            icon: 'pi pi-id-card',
            routerLink: '/person/b767bf49-6ca8-4d4b-ad87-d5f224e50ca2',
          },
          {
            label: 'Mia Mustermann',
            icon: 'pi pi-id-card',
            routerLink: '/person/0c4f1a2b-3d7e-4f8b-9c5d-6a2e0f3b5c1d',
          },
        ],
      },
      { separator: true },
      {
        label: 'Administration',
        items: [
          {
            label: 'Nutzerverwaltung',
            icon: 'pi pi-user',
            routerLink: '/administration/user-management',
          },
          {
            label: 'NaMi',
            icon: 'pi pi-link',
            routerLink: '/administration/nami',
          },
          {
            label: 'Einstellungen',
            icon: 'pi pi-cog',
            routerLink: '/administration/settings',
          },
        ],
      },
    ];
  }

  constructor(private readonly http: HttpClient) {}

  protected logout(): void {
    this.http.post('/api/logout', {}).subscribe({
      next: () => {
        window.location.href = '/';
      },
      error: (err) => {
        console.error('Logout failed', err);
      },
    });
  }
}
