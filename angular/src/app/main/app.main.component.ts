import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { InputSwitchOnChangeEvent } from 'primeng/inputswitch/inputswitch';
import { MenuService } from '../menu/app.menu.service';
import { AppComponent } from '../app.component';

@Component({
  selector: 'at-main',
  templateUrl: './app.main.component.html',
})
export class AppMainComponent implements OnInit {

  topbarMenuActive = false;

  overlayMenuActive = false;

  staticMenuDesktopInactive = false;

  staticMenuMobileActive = false;

  menuClick = false;

  topbarItemClick = false;

  activeTopbarItem: any;

  menuHoverActive = false;

  rightPanelActive = false;

  rightPanelClick = false;

  topbarIconsActive = false;

  quickMenuButtonClick = false;

  configActive = false;

  configClick = false;

  constructor(private menuService: MenuService, private primengConfig: PrimeNGConfig, public app: AppComponent) { }

  ngOnInit() {
    this.primengConfig.ripple = true;
  }

  onLayoutClick() {
    if (!this.topbarItemClick) {
      this.activeTopbarItem = null;
      this.topbarMenuActive = false;
    }

    if (!this.rightPanelClick) {
      this.rightPanelActive = false;
    }

    if (!this.quickMenuButtonClick) {
      this.quickMenuButtonClick = false;
      this.topbarIconsActive = false;
    }

    if (!this.menuClick) {
      if (this.isHorizontal() || this.isSlim()) {
        this.menuService.reset();
      }

      if (this.overlayMenuActive || this.staticMenuMobileActive) {
        this.hideOverlayMenu();
      }

      this.menuHoverActive = false;
    }

    if (this.configActive && !this.configClick) {
      this.configActive = false;
    }

    this.configClick = false;
    this.topbarItemClick = false;
    this.quickMenuButtonClick = false;
    this.menuClick = false;
    this.rightPanelClick = false;
  }

  onMenuButtonClick(event: MouseEvent) {
    this.menuClick = true;
    this.topbarMenuActive = false;

    if (this.isOverlay()) {
      this.overlayMenuActive = !this.overlayMenuActive;
    }
    if (this.isDesktop()) {
      this.staticMenuDesktopInactive = !this.staticMenuDesktopInactive;
    } else {
      this.staticMenuMobileActive = !this.staticMenuMobileActive;
    }

    event.preventDefault();
  }

  onQuickMenuButtonClick(event: MouseEvent) {
    if (this.isMobile()) {
      this.topbarIconsActive = !this.topbarIconsActive;
      this.quickMenuButtonClick = true;
    }
    event.preventDefault();
  }

  onMenuClick() {
    this.menuClick = true;
  }

  onTopbarMenuButtonClick(event: MouseEvent) {
    this.topbarItemClick = true;
    this.topbarMenuActive = !this.topbarMenuActive;

    this.hideOverlayMenu();

    event.preventDefault();
  }

  onTopbarItemClick(event: MouseEvent, item: any) {
    this.topbarItemClick = true;

    if (this.activeTopbarItem === item) {
      this.activeTopbarItem = null;
    } else {
      this.activeTopbarItem = item;
    }

    event.preventDefault();
  }

  onTopbarSubItemClick(event: MouseEvent) {
    event.preventDefault();
  }

  onRightPanelButtonClick(event: MouseEvent) {
    this.rightPanelClick = true;
    this.rightPanelActive = !this.rightPanelActive;
    event.preventDefault();
  }

  onRightPanelClick() {
    this.rightPanelClick = true;
  }

  onRippleChange(event: InputSwitchOnChangeEvent) {
    this.app.ripple = event.checked;
  }

  isHorizontal() {
    return this.app.menuMode === 'horizontal';
  }

  isSlim() {
    return this.app.menuMode === 'slim';
  }

  isOverlay() {
    return this.app.menuMode === 'overlay';
  }

  isStatic() {
    return this.app.menuMode === 'static';
  }

  isMobile() {
    return window.innerWidth < 1025;
  }

  isDesktop() {
    return window.innerWidth > 1024;
  }

  isTablet() {
    const width = window.innerWidth;
    return width <= 1024 && width > 640;
  }

  hideOverlayMenu() {
    this.overlayMenuActive = false;
    this.staticMenuMobileActive = false;
  }
}