import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { marker as t } from '@ngneat/transloco-keys-manager/marker';
import { AuthenticationService } from "src/app/services/authentication.service";

export interface ListItem {
  text: string;
  href: string;
  icon: string;
}


const navigationItemsBuyer: ListItem[] = [
  { text: t('menu.home'), href: '/home', icon: 'fa fa-house' },
  { text: t('menu.vehicle'), href: '/vehicle', icon: 'fa fa-car' },
  { text: t('menu.vehicle-bids'), href: '/vehicle/bids', icon: 'fa-solid fa-caret-right' },
  { text: t('menu.vehicle-won'), href: '/vehicle/won', icon: 'fa-solid fa-check' },
  { text: t('menu.vehicle-lost'), href: '/vehicle/lost', icon: 'fa-solid fa-xmark' },
  { text: t('menu.watch-list'), href: '/vehicle/watchlist', icon: 'fa fa-star' },
  { text: t('menu.payment'), href: '/payment', icon: 'fa-regular fa-credit-card' },
  { text: t('menu.calendar'), href: '/calendar', icon: 'fa-solid fa-calendar-days' },
  { text: t('menu.auction-list'), href: '/auction/list', icon: 'fa fa-gavel' },
  { text: t('menu.auction'), href: '/auction', icon: 'fa-solid fa-globe' },
  { text: t('menu.location'), href: '/location', icon: 'fa fa-location-arrow' },
  { text: t('menu.services'), href: '/services', icon: 'fa fa-server' },
  { text: t('menu.support'), href: '/support', icon: 'fa fa-info-circle' },
];


const navigationItemsSeller: ListItem[] = [
  { text: t('menu.home'), href: '/home', icon: 'fa fa-house' },
  { text: t('menu.vehicle'), href: '/vehicle', icon: 'fa fa-car' },
  { text: t('menu.vehicle-add'), href: '/vehicle/add', icon: 'fa-solid fa-plus' },
  { text: t('menu.watch-list'), href: '/vehicle/watchlist', icon: 'fa fa-star' },
  { text: t('menu.payment'), href: '/payment', icon: 'fa-regular fa-credit-card' },
  { text: t('menu.calendar'), href: '/calendar', icon: 'fa-solid fa-calendar-days' },
  { text: t('menu.services'), href: '/services', icon: 'fa fa-server' },
  { text: t('menu.support'), href: '/support', icon: 'fa fa-info-circle' },
];


const navigationItemsAdmin: ListItem[] = [
  { text: t('menu.home'), href: '/home', icon: 'fa fa-house' },
  { text: t('menu.vehicle'), href: '/vehicle', icon: 'fa fa-car' },
  { text: t('menu.watch-list'), href: '/vehicle/watchlist', icon: 'fa fa-star' },
  { text: t('menu.calendar'), href: '/calendar', icon: 'fa-solid fa-calendar-days' },
  { text: t('menu.auction'), href: '/auction', icon: 'fa-solid fa-globe' },
  { text: t('menu.location'), href: '/location', icon: 'fa fa-location-arrow' },
  { text: t('menu.verification-vehicle'), href: '/vehicle/verification', icon: 'fa-solid fa-square-check' },
  { text: t('menu.panel-vehicle'), href: '/vehicle/panel', icon: 'fa-solid fa-wrench' },
  { text: t('menu.panel-auction'), href: '/auction/panel', icon: 'fa-solid fa-toolbox' },
];

const navigationItemsDefault: ListItem[] = [
  { text: t('menu.home'), href: '/home', icon: 'fa fa-house' },
  { text: t('menu.vehicle'), href: '/vehicle', icon: 'fa fa-car' },
  { text: t('menu.auction-list'), href: '/auction/list', icon: 'fa fa-gavel' },
  { text: t('menu.auction'), href: '/auction', icon: 'fa-solid fa-globe' },
  { text: t('menu.location'), href: '/location', icon: 'fa fa-location-arrow' },
  { text: t('menu.services'), href: '/services', icon: 'fa fa-server' },
  { text: t('menu.support'), href: '/support', icon: 'fa fa-info-circle' },
];

@Component({
  selector: 'auction-nav',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  navigation: ListItem[];

  constructor(private authenticationService: AuthenticationService,
    private router: Router) {
    this.navigation = navigationItemsDefault;
  }
  ngOnInit(): void {
    this.router.events.subscribe((value: any) => {
      if (value.url) {
        this.authenticationService.loggedUserId().subscribe({
          next: (res: any) => {
            this.authenticationService.getUserRole(res.userId).subscribe( res => { 
              if(res.name=="Buyer"){
                this.navigation = navigationItemsBuyer;
              }

              if(res.name=="Seller"){
                this.navigation = navigationItemsSeller;
              }

              if(res.name=="Admin"){
                this.navigation = navigationItemsAdmin;
              }
            });
          },
          error: (err: any) => {
            this.navigation = navigationItemsDefault;
          }
        })
      }
    });
  }
}