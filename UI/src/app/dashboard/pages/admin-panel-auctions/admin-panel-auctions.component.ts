import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuctionService } from 'src/app/common/services/auction.service';

@Component({
  selector: 'app-admin-panel-auctions',
  templateUrl: './admin-panel-auctions.component.html',
  styleUrls: ['./admin-panel-auctions.component.css']
})
export class AdminPanelAuctionsComponent  {
  datasource: any;

  readonly allowedPageSizes = [5, 10, 20, 'all'];

  displayMode = 'full';

  constructor(private auctionService: AuctionService,
    private router: Router) {
    this.auctionService.getAuctionList().subscribe(res => {
      this.datasource = res;
    });
  }


  editClick(auctionId: any)  {
    this.router.navigate(['/auction/edit', auctionId].filter(v => !!v));
  }

  deleteClick(auctionId: any)  {
    this.auctionService.deleteAuction(auctionId).subscribe(res => {
      this.auctionService.getAuctionList().subscribe(res => {
        this.datasource = res;
      });
    })
  }


}
