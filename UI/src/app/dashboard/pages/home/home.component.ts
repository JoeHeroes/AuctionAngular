import { Component, OnInit } from '@angular/core';
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
export class HomeComponent implements OnInit {
  customizeText(arg: any) {
    return `${arg.valueText} %`;
  }


  constructor(private system: AuctionSystemApiClient) {
  }

  ngOnInit(): void {
  }

}