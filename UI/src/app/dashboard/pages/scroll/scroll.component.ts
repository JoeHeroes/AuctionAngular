import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { AuctionService } from 'src/app/common/services/auction.service';

@Component({
  selector: 'app-scroll',
  templateUrl: './scroll.component.html',
  styleUrls: ['./scroll.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ScrollComponent implements OnInit {

  datasource: any;

  constructor(
    private service: AuctionService,
  ) {
  }


  ngOnInit(): void {
    this.service.liveAuctionList().subscribe(res => {
      this.datasource = res;
    });
  }
  items = Array.from({ length: 100000 }).map((_, i) => `Item #${i}`);

}
