import { Component, ViewChild } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { Drawer, DrawerModule } from 'primeng/drawer';
import { MainMenuComponent } from '../menu/main-menu/main-menu.component';

@Component({
  selector: 'sv-main',
  imports: [MainMenuComponent, DrawerModule, DialogModule],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss',
})
export class MainComponent {
  @ViewChild('menuDrawer') menuDrawer: Drawer | undefined;

  protected drawerMenuOpen = false;

  protected closeDrawer(e: Event) {
    this.menuDrawer?.close(e);
  }
}
