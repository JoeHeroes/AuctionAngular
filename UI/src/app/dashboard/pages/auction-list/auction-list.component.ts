import { Component } from '@angular/core';
import { AuctionService } from 'src/app/common/services/auction.service';

@Component({
  selector: 'app-auction-list',
  templateUrl: './auction-list.component.html',
  styleUrls: ['./auction-list.component.css']
})
export class AuctionListComponent {
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private auctionService: AuctionService) {
    this.auctionService.getAuctionList().subscribe(res => {
      this.datasource = res;
    });
  }
}