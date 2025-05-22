import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { AvatarModule } from 'primeng/avatar';
import { BadgeModule } from 'primeng/badge';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';

@Component({
  selector: 'sv-main-menu',
  imports: [MenuModule, BadgeModule, AvatarModule, ButtonModule],
  templateUrl: './main-menu.component.html',
  styleUrl: './main-menu.component.scss',
})
export class MainMenuComponent implements OnInit {
  @Output()
  public close = new EventEmitter<Event>();

  items: MenuItem[] | undefined;

  public ngOnInit() {
    this.items = [
      { separator: true },
      {
        label: 'Documents',
        items: [
          {
            label: 'New',
            icon: 'pi pi-plus',
            shortcut: '⌘+N',
          },
          {
            label: 'Search',
            icon: 'pi pi-search',
            shortcut: '⌘+S',
          },
        ],
      },
      { separator: true },
    ];
  }
}
