import { Component, ViewChild } from '@angular/core';
import {
  ActivatedRoute,
  NavigationEnd,
  Router,
  RouterOutlet,
} from '@angular/router';
import { DialogModule } from 'primeng/dialog';
import { Drawer, DrawerModule } from 'primeng/drawer';
import { filter, map, mergeMap } from 'rxjs';
import { MainMenuComponent } from '../menu/main-menu/main-menu.component';

@Component({
  selector: 'sv-main',
  imports: [MainMenuComponent, DrawerModule, DialogModule, RouterOutlet],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss',
})
export class MainComponent {
  @ViewChild('menuDrawer') menuDrawer: Drawer | undefined;

  protected currentTitle = '';
  protected drawerMenuOpen = false;

  constructor(
    private readonly router: Router,
    private readonly activatedRoute: ActivatedRoute
  ) {
    this.router.events
      .pipe(
        filter((event) => event instanceof NavigationEnd),
        map(() => {
          let route = this.activatedRoute;
          while (route.firstChild) route = route.firstChild;
          return route;
        }),
        mergeMap((route) => route.data)
      )
      .subscribe((data) => {
        this.currentTitle = data['title'] || 'ScoutVenture';
      });
  }

  protected closeDrawer(e: Event) {
    this.menuDrawer?.close(e);
  }
}
