import { Component, OnInit } from '@angular/core';
import { AuctionService } from 'src/app/common/services/auction.service';
import { ScrollingModule } from '@angular/cdk/scrolling';


@Component({
  selector: 'app-auction',
  templateUrl: './auction.component.html',
  styleUrls: ['./auction.component.css']
})
export class AuctionComponent implements OnInit {


  liveAuction: boolean = false;
  datasource: any;

  pictures: any;
  slideshowDelay = 2000;



  constructor(
    private service: AuctionService,
  ) {
  }


  ngOnInit(): void {

    window.scroll(0, 0);
    //for the first time

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
