import { Component, OnInit } from '@angular/core';
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
  { id: 50, text: _('menu.auction'), href: '/auction', icon: 'fa fa-gavel' },
  { id: 70, text: _('menu.location'), href: '/location', icon: 'fa fa-location-arrow' },
  { id: 90, text: _('menu.services'), href: '/services', icon: 'fa fa-server' },
  { id: 120, text: _('menu.privacy'), href: '/privacy', icon: 'fa fa-user-secret' },
];

@Component({
  selector: 'auction-nav',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss', './nav-bar.component.dark.scss']
})
export class NavBarComponent implements OnInit {
  navigation: ListItem[];

  constructor() {
    this.navigation = navigationItems
  }

  ngOnInit(): void {
  }

}