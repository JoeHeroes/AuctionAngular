import { Component } from "@angular/core";
import { marker as _ } from '@ngneat/transloco-keys-manager/marker';

export interface ListItem {
  text: string;
  href: string;
  icon: string;
}


const navigationItems: ListItem[] = [
  { text: _('menu.home'), href: '/home', icon: 'fa fa-house' },
  { text: _('menu.vehicle'), href: '/vehicle', icon: 'fa fa-car' },
  { text: _('menu.vehicle-bids'), href: '/vehicle/bids', icon: '' },
  { text: _('menu.vehicle-won'), href: '/vehicle/won', icon: '' },
  { text: _('menu.vehicle-lost'), href: '/vehicle/lost', icon: '' },
  { text: _('menu.watch-list'), href: '/vehicle/watchlist', icon: 'fa fa-star' },
  { text: _('menu.payment'), href: '/payment', icon: 'fa-regular fa-credit-card' },
  { text: _('menu.calendar'), href: '/calendar', icon: 'fa-solid fa-calendar-days' },
  { text: _('menu.auction-list'), href: '/auctionlist', icon: 'fa fa-gavel' },
  { text: _('menu.auction'), href: '/auction', icon: 'fa-solid fa-globe' },
  { text: _('menu.location'), href: '/location', icon: 'fa fa-location-arrow' },
  { text: _('menu.services'), href: '/services', icon: 'fa fa-server' },
  { text: _('menu.support'), href: '/support', icon: 'fa fa-info-circle' },
  { text: _('menu.admin'), href: '/panel', icon: 'fa-solid fa-toolbox' },

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