import { Component, OnInit } from '@angular/core';
import { AuctionService } from 'src/app/common/services/auction.service';

@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.css']
})
export class AuctionComponent implements OnInit {

  liveAuction: boolean = false;
  datasource: any;
  pictures: any;
  slideshowDelay = 3000;

  constructor(private service: AuctionService) {
  }

  ngOnInit(): void {

    window.scroll(0, 0);
    this.service.liveAuction().subscribe(res => {
      this.liveAuction = res;
      if (res) {
        this.service.liveAuctionList().subscribe(res => {
          this.datasource = res;
        });
      }
    });
  }
}
