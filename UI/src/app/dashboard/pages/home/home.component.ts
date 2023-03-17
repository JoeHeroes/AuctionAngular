import { Component, OnInit, OnDestroy } from '@angular/core';
import { marker as _ } from '@ngneat/transloco-keys-manager/marker';
import { AuctionSystemApiClient } from 'src/app/common/services/auction-api.generated.service';

export interface cardItem {
  text: string;
  href: string;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {


  constructor(private system: AuctionSystemApiClient) {
  }

  ngOnDestroy(): void {
  }

  ngOnInit(): void {
  }

}