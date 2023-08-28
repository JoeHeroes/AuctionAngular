import { Component } from "@angular/core";
import { marker as _ } from '@ngneat/transloco-keys-manager/marker';

export interface ListItem {
  id: number;
  text: string;
  href: string;
  icon: string;
}


const navigationItems: ListItem[] = [
  { id: 10, text: _('menu.home'), href: '/home', icon: 'fa fa-house' },
  { id: 30, text: _('menu.vehicle'), href: '/vehicle', icon: 'fa fa-car' },
  { id: 30, text: _('menu.vehicle-bids'), href: '/vehicle/bids', icon: '' },
  { id: 50, text: _('menu.vehicle-won'), href: '/vehicle/won', icon: '' },
  { id: 70, text: _('menu.vehicle-lost'), href: '/vehicle/lost', icon: '' },
  { id: 90, text: _('menu.watchlist'), href: '/vehicle/watchlist', icon: 'fa fa-star' },
  { id: 110, text: _('menu.payment'), href: '/payment', icon: 'fa-regular fa-credit-card' },
  { id: 130, text: _('menu.calendar'), href: '/calendar', icon: 'fa-solid fa-calendar-days' },
  { id: 150, text: _('menu.auction'), href: '/auction', icon: 'fa fa-gavel' },
  { id: 170, text: _('menu.location'), href: '/location', icon: 'fa fa-location-arrow' },
  { id: 190, text: _('menu.services'), href: '/services', icon: 'fa fa-server' },
  { id: 210, text: _('menu.support'), href: '/support', icon: 'fa fa-info-circle' },
  { id: 240, text: _('menu.admin'), href: '/panel', icon: 'fa-solid fa-toolbox' },

];

@Component({
  selector: 'auction-nav',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  navigation: ListItem[];

  constructor() {
    this.navigation = navigationItems
  }
}